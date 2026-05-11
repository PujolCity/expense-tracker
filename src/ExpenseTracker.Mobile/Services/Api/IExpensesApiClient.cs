using ExpenseTracker.Mobile.Models.Expenses;

namespace ExpenseTracker.Mobile.Services.Api;

public interface IExpensesApiClient
{
    Task<List<ExpenseResponse>> GetAsync();
    Task CreateExpenseAsync(CreateExpenseRequest request);
    Task UpdateExpenseAsync(Guid id, UpdateExpenseRequest request);
    Task<ExpenseResponse?> GetExpenseByIdAsync(Guid id);
}
