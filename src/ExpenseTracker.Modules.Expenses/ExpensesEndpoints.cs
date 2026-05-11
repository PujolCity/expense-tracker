using ExpenseTracker.Modules.Expenses.CreateExpense;
using ExpenseTracker.Modules.Expenses.DeleteExpense;
using ExpenseTracker.Modules.Expenses.GetExpenseById;
using ExpenseTracker.Modules.Expenses.GetExpenses;
using ExpenseTracker.Modules.Expenses.UpdateExpense;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Modules.Expenses;

public static class ExpensesEndpoints
{
    public static IEndpointRouteBuilder MapExpensesEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapCreateExpense();
        app.MapGetExpenses();
        app.MapDeleteExpense();
        app.MapUpdateExpense();
        app.MapGetExpenseById();

        return app;
    }
}
