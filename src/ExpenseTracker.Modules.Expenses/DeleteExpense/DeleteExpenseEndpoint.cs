using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Modules.Expenses.DeleteExpense;

public static class DeleteExpenseEndpoint
{
    public static IEndpointRouteBuilder MapDeleteExpense(
        this IEndpointRouteBuilder app)
    {
        app.MapDelete("/expenses/{id:guid}",
            async (
                Guid id,
                DeleteExpenseHandler handler,
                CancellationToken cancellationToken) =>
            {
                var deleted = await handler.HandleAsync(id, cancellationToken);

                return deleted
                    ? Results.NoContent()
                    : Results.NotFound();
            });

        return app;
    }
}