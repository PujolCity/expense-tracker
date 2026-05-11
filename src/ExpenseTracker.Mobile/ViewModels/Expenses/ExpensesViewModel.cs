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

    public ExpensesViewModel(IExpensesApiClient expensesApiClient)
    {
        _expensesApiClient = expensesApiClient;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var result = await _expensesApiClient.GetAsync();

        Expenses = new ObservableCollection<ExpenseResponse>(result);
    }

    [RelayCommand]
    private async Task GoToCreateExpenseAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateExpensePage));
    }
}
