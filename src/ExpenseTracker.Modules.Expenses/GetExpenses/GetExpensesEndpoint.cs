using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Modules.Expenses.GetExpenses;

public static class GetExpensesEndpoint
{
    public static IEndpointRouteBuilder MapGetExpenses(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/expenses",
            async (
                GetExpensesHandler handler,
                CancellationToken cancellationToken) =>
            {
                var expenses = await handler.HandleAsync(cancellationToken);

                return Results.Ok(expenses);
            });

        return app;
    }
}
