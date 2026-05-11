using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Modules.Expenses.GetExpenseById;

public static class GetExpenseByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetExpenseById(
       this IEndpointRouteBuilder app)
    {
        app.MapGet("/expenses/{id:guid}",
            async (
                Guid id,
                GetExpenseByIdHandler handler,
                CancellationToken cancellationToken) =>
            {
                var expense = await handler.HandleAsync(id, cancellationToken);

                if (expense is null)
                    return Results.NotFound();

                return Results.Ok(expense);
            });

        return app;
    }
}
