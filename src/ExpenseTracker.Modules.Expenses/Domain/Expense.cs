namespace ExpenseTracker.Modules.Expenses.Domain;

public sealed class Expense
{
    public Guid Id { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public decimal Amount { get; private set; }

    public DateTimeOffset Date { get; private set; }

    private Expense()
    {
    }

    public Expense(
        string description,
        decimal amount,
        DateTimeOffset date)
    {
        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        Date = date.ToUniversalTime();
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