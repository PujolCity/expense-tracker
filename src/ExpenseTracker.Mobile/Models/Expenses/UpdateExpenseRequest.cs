namespace ExpenseTracker.Mobile.Models.Expenses;

public sealed class UpdateExpenseRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
