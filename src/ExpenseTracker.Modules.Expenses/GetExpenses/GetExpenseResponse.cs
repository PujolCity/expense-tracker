namespace ExpenseTracker.Modules.Expenses.GetExpenses;

public sealed record GetExpenseResponse(
    Guid Id,
    string Description,
    decimal Amount,
    DateTimeOffset Date);