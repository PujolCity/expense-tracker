using ExpenseTracker.Mobile.Services.Api;
using ExpenseTracker.Mobile.ViewModels.Expenses;
using ExpenseTracker.Mobile.Views.Expenses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

        using var appSettingsStream = FileSystem
        .OpenAppPackageFileAsync("appsettings.json")
        .Result;

        builder.Configuration.AddJsonStream(appSettingsStream);

#if ANDROID
        using var androidSettingsStream = FileSystem
            .OpenAppPackageFileAsync("appsettings.Android.json")
            .Result;

        builder.Configuration.AddJsonStream(androidSettingsStream);
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(ApiSettings.SectionName));
        builder.Services.AddSingleton<ExpensesViewModel>();

        builder.Services.AddSingleton<ExpensesPage>();

        builder.Services.AddHttpClient<IExpensesApiClient, ExpensesApiClient>(
                        (services, client) =>
                        {
                            var settings = services
                                .GetRequiredService<IOptions<ApiSettings>>()
                                .Value;

                            client.BaseAddress = new Uri(settings.BaseUrl);
                        });

        builder.Services.AddTransient<CreateExpensePage>();
        builder.Services.AddTransient<CreateExpenseViewModel>();

        return builder.Build();
    }
}
