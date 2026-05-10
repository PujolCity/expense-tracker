using ExpenseTracker.Modules.Expenses.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Modules.Expenses.UpdateExpense;

public sealed class UpdateExpenseHandler
{
    private readonly ExpensesDbContext _dbContext;

    public UpdateExpenseHandler(ExpensesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HandleAsync(
        Guid id,
        UpdateExpenseRequest request,
        CancellationToken cancellationToken = default)
    {
        var expense = await _dbContext.Expenses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (expense is null)
        {
            return false;
        }

        expense.Update(
            request.Description,
            request.Amount,
            request.Date);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}