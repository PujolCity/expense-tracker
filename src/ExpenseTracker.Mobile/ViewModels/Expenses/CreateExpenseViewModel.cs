using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseTracker.Mobile.Models.Expenses;
using ExpenseTracker.Mobile.Services.Api;

namespace ExpenseTracker.Mobile.ViewModels.Expenses;

public partial class CreateExpenseViewModel : ObservableObject
{
    private readonly IExpensesApiClient _expensesApiClient;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string amount = string.Empty;

    [ObservableProperty]
    private DateTime date = DateTime.Now;

    [ObservableProperty]
    private bool isBusy;

    public CreateExpenseViewModel(IExpensesApiClient expensesApiClient)
    {
        _expensesApiClient = expensesApiClient;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrWhiteSpace(Description))
        {
            await Shell.Current.DisplayAlertAsync("Validación", "Ingresá una descripción.", "OK");
            return;
        }

        if (!decimal.TryParse(Amount, out var parsedAmount) || parsedAmount <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Validación", "Ingresá un monto válido.", "OK");
            return;
        }

        if(Date > DateTime.Now)
        {
            await Shell.Current.DisplayAlertAsync("Validación", "La fecha no puede ser futura.", "OK");
            return;
        }

        try
        {
            IsBusy = true;

            var request = new CreateExpenseRequest
            {
                Description = Description.Trim(),
                Amount = parsedAmount,
                Date = Date
            };

            await _expensesApiClient.CreateExpenseAsync(request);

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
