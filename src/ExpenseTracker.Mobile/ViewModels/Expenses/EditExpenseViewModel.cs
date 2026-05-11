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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Id", out var value))
            return;

        _expenseId = Guid.Parse(value.ToString()!);

        var expense = await _apiClient.GetExpenseByIdAsync(_expenseId);

        Description = expense?.Description ?? string.Empty;
        Amount = expense?.Amount.ToString() ?? string.Empty;
        Date = expense?.Date ?? DateTime.Today;
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

        var expense = await _apiClient.GetExpenseByIdAsync(_expenseId);

        if (expense is null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "No se encontró el gasto.",
                "OK");

            await Shell.Current.GoToAsync("..");

            return;
        }

        await _apiClient.UpdateExpenseAsync(_expenseId, request);

        await Shell.Current.GoToAsync("..");
    }
}
