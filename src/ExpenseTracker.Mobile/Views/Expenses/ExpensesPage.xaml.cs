using ExpenseTracker.Mobile.ViewModels.Expenses;

namespace ExpenseTracker.Mobile.Views.Expenses;

public partial class ExpensesPage : ContentPage
{
    private readonly ExpensesViewModel _viewModel;

    public ExpensesPage(ExpensesViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadAsync();
    }
}
