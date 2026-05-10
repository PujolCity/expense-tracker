using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Modules.Expenses.UpdateExpense;

public static class UpdateExpenseEndpoint
{
    public static IEndpointRouteBuilder MapUpdateExpense(
        this IEndpointRouteBuilder app)
    {
        app.MapPatch("/expenses/{id:guid}",
            async (
                Guid id,
                UpdateExpenseRequest request,
                UpdateExpenseHandler handler,
                UpdateExpenseValidator validator,
                CancellationToken cancellationToken) =>
            {
                var validationResult =
                    await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(
                        validationResult.ToDictionary());
                }

                var updated = await handler.HandleAsync(
                    id,
                    request,
                    cancellationToken);

                return updated
                    ? Results.NoContent()
                    : Results.NotFound();
            });

        return app;
    }
}