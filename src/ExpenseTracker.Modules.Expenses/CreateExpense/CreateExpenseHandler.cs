using ExpenseTracker.Modules.Expenses.Domain;
using ExpenseTracker.Modules.Expenses.Persistence;

namespace ExpenseTracker.Modules.Expenses.CreateExpense;

public sealed class CreateExpenseHandler
{
    private readonly ExpensesDbContext _dbContext;

    public CreateExpenseHandler(ExpensesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateExpenseResponse> HandleAsync(
       CreateExpenseRequest request,
       CancellationToken cancellationToken = default)
    {
        var expense = new Expense(
            request.Description,
            request.Amount,
            request.Date);

        _dbContext.Expenses.Add(expense);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateExpenseResponse(expense.Id);
    }
}
