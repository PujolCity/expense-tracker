using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Modules.Expenses.CreateExpense;

public static class CreateExpenseEndpoint
{
    public static IEndpointRouteBuilder MapCreateExpense(
       this IEndpointRouteBuilder app)
    {
        app.MapPost("/expenses",
            async (
                CreateExpenseRequest request,
                CreateExpenseHandler handler,
                CreateExpenseValidator validator,
                CancellationToken cancellationToken) =>
            {
                var validationResult =
                    await validator.ValidateAsync(
                        request,
                        cancellationToken);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(
                        validationResult.ToDictionary());
                }

                var response =
                    await handler.HandleAsync(
                        request,
                        cancellationToken);

                return Results.Created(
                    $"/expenses/{response.Id}",
                    response);
            });

        return app;
    }
}
