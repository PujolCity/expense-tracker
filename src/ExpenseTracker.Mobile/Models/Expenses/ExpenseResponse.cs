namespace ExpenseTracker.Mobile.Models.Expenses;

public class ExpenseResponse
{
    public Guid Id { get; set; }

    public string Description { get; set; } = default!;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}
