using FluentValidation;

namespace ExpenseTracker.Modules.Expenses.UpdateExpense;

public sealed class UpdateExpenseValidator
    : AbstractValidator<UpdateExpenseRequest>
{
    public UpdateExpenseValidator()
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