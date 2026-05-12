using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseTracker.Mobile.Models.Expenses;
using ExpenseTracker.Mobile.Services.Api;

namespace ExpenseTracker.Mobile.ViewModels.Expenses;

public partial class EditExpenseViewModel :
    ObservableObject,
    IQueryAttributable
{
    private readonly IExpensesApiClient _apiClient;

    private Guid _expenseId;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string amount = string.Empty;

    [ObservableProperty]
    private DateTime date = DateTime.Today;

    public DateTime MaxDate => DateTime.Today;

    public EditExpenseViewModel(IExpensesApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public void ApplyQueryAttributes(
       IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Expense", out var value))
            return;

        if (value is not ExpenseResponse expense)
            return;

        _expenseId = expense.Id;

        Description = expense.Description;
        Amount = expense.Amount.ToString("0.##");
        Date = expense.Date;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!decimal.TryParse(Amount, out var parsedAmount))
            return;

        var request = new UpdateExpenseRequest
        {
            Description = Description,
            Amount = parsedAmount,
            Date = Date
        };

        await _apiClient.UpdateExpenseAsync(_expenseId, request);

        await Shell.Current.GoToAsync("..");
    }
}
