using ExpenseTracker.Modules.Expenses.CreateExpense;
using ExpenseTracker.Modules.Expenses.DeleteExpense;
using ExpenseTracker.Modules.Expenses.GetExpenseById;
using ExpenseTracker.Modules.Expenses.GetExpenses;
using ExpenseTracker.Modules.Expenses.Persistence;
using ExpenseTracker.Modules.Expenses.UpdateExpense;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Modules.Expenses.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddExpensesModule(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddDbContext<ExpensesDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<CreateExpenseHandler>();
        services.AddScoped<CreateExpenseValidator>();

        services.AddScoped<GetExpensesHandler>();
        services.AddScoped<GetExpenseByIdHandler>();

        services.AddScoped<DeleteExpenseHandler>();

        services.AddScoped<UpdateExpenseHandler>();
        services.AddScoped<UpdateExpenseValidator>();

        return services;
  //  < ItemGroup >
  //  < FrameworkReference Include = "Microsoft.AspNetCore.App" />
  //</ ItemGroup >


    }
}
