using FluentValidation;

namespace ExpenseTracker.Modules.Expenses.CreateExpense;

public sealed class CreateExpenseValidator
    : AbstractValidator<CreateExpenseRequest>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.Date)
            .NotEmpty();
    }
}
