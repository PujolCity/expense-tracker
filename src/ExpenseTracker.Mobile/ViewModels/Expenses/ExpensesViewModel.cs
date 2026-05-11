using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseTracker.Mobile.Models.Expenses;
using ExpenseTracker.Mobile.Services.Api;
using ExpenseTracker.Mobile.Views.Expenses;

namespace ExpenseTracker.Mobile.ViewModels.Expenses;

public partial class ExpensesViewModel : ObservableObject
{
    private readonly IExpensesApiClient _expensesApiClient;

    [ObservableProperty]
    public partial ObservableCollection<ExpenseResponse> Expenses { get; set; } = [];

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string? errorMessage;

    public ExpensesViewModel(IExpensesApiClient expensesApiClient)
    {
        _expensesApiClient = expensesApiClient;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        try
        {
            IsRefreshing = true;
            ErrorMessage = null;

            var result = await _expensesApiClient.GetAsync();

            Expenses = new ObservableCollection<ExpenseResponse>(result);
        }
        catch (Exception ex)
        {
            ErrorMessage = "No se pudieron cargar los gastos.";
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task GoToCreateExpenseAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateExpensePage));
    }

    [RelayCommand]
    public async Task GoToEditExpenseAsync(ExpenseResponse expense)
    {
        await Shell.Current.GoToAsync(nameof(EditExpensePage),
            new Dictionary<string, object> { ["Expense"] = expense });
    }

    [RelayCommand]
    public async Task DeleteExpenseAsync(ExpenseResponse expense)
    {
        if (expense is null)
            return;

        var confirm = await Shell.Current.DisplayAlertAsync(
            "Eliminar gasto",
            $"¿Seguro que querés eliminar \"{expense.Description}\"?",
            "Eliminar",
            "Cancelar");

        if (!confirm)
            return;

        await _expensesApiClient.DeleteAsync(expense.Id);

        Expenses.Remove(expense);
    }
}
