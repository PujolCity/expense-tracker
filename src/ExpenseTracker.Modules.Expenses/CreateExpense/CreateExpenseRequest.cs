namespace ExpenseTracker.Modules.Expenses.CreateExpense;

public sealed record CreateExpenseRequest(
    string Description,
    decimal Amount,
    DateTimeOffset Date,
    Guid CategoryId);
