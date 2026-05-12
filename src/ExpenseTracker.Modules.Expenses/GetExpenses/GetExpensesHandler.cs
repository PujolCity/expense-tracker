using ExpenseTracker.Modules.Expenses.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Modules.Expenses.GetExpenses;

public sealed class GetExpensesHandler
{
    private readonly ExpensesDbContext _dbContext;

    public GetExpensesHandler(ExpensesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetExpenseResponse>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .Include(x => x.Category)
            .OrderByDescending(x => x.Date)
            .Select(x => new GetExpenseResponse(
                x.Id,
                x.Description,
                x.Amount,
                x.Date,
                x.CategoryId,
                x.Category.Name))
            .ToListAsync(cancellationToken);
    }
}
