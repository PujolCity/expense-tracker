using ExpenseTracker.Mobile.Models.Expenses;

namespace ExpenseTracker.Mobile.Services.Api;

public interface IExpensesApiClient
{
    Task<List<ExpenseResponse>> GetAsync();
}
