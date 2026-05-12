namespace ExpenseTracker.Modules.Expenses.Domain;

public sealed class Expense
{
    public Guid Id { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public DateTimeOffset Date { get; private set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    private Expense()
    {
    }

    public Expense(
        string description,
        decimal amount,
        DateTimeOffset date,
        Guid category)
    {
        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        Date = date.ToUniversalTime();
        CategoryId = category;
    }

    public void Update(
    string description,
    decimal amount,
    DateTimeOffset date)
    {
        Description = description;
        Amount = amount;
        Date = date.ToUniversalTime();
    }
}
