using ExpenseTracker.Modules.Expenses.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Modules.Expenses.DeleteExpense;

public sealed class DeleteExpenseHandler
{
    private readonly ExpensesDbContext _dbContext;

    public DeleteExpenseHandler(ExpensesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HandleAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var expense = await _dbContext.Expenses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (expense is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(expense);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
