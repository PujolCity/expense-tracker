namespace ExpenseTracker.Modules.Expenses.UpdateExpense;

public sealed record UpdateExpenseRequest(
    string Description,
    decimal Amount,
    DateTimeOffset Date);