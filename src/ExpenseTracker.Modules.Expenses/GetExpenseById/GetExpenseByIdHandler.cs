using ExpenseTracker.Modules.Expenses.GetExpenses;
using ExpenseTracker.Modules.Expenses.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Modules.Expenses.GetExpenseById;

public sealed class GetExpenseByIdHandler
{
    private readonly ExpensesDbContext _dbContext;

    public GetExpenseByIdHandler(ExpensesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetExpenseResponse?> HandleAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var expense = await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (expense is null)
            return null;

        return new GetExpenseResponse(
            expense.Id,
            expense.Description,
            expense.Amount,
            expense.Date);
    }
}
