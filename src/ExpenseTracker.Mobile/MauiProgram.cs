using ExpenseTracker.Mobile.Services.Api;
using ExpenseTracker.Mobile.ViewModels.Expenses;
using ExpenseTracker.Mobile.Views.Expenses;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var configFile = GetConfigFileName();

        using var stream = FileSystem.OpenAppPackageFileAsync(configFile).GetAwaiter().GetResult();

        var configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(configuration);

        builder.Services.Configure<ApiSettings>(
            builder.Configuration.GetSection(nameof(ApiSettings)));

        var apiSettings = builder.Configuration
            .GetSection(nameof(ApiSettings))
            .Get<ApiSettings>()!;

        builder.Services.AddHttpClient<IExpensesApiClient, ExpensesApiClient>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.BaseUrl);
        });

        builder.Services.AddSingleton<ExpensesViewModel>();
        builder.Services.AddSingleton<ExpensesPage>();
        builder.Services.AddTransient<CreateExpensePage>();
        builder.Services.AddTransient<CreateExpenseViewModel>();
        builder.Services.AddTransient<EditExpensePage>();
        builder.Services.AddTransient<EditExpenseViewModel>();

        return builder.Build();
    }

    static string GetConfigFileName()
    {
#if RELEASE
    return "appsettings.Production.json";
#else
        if (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Physical)
        {
            return "appsettings.AndroidPhysical.json";
        }

        return "appsettings.Development.json";
#endif
    }
}
