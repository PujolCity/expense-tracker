using ExpenseTracker.Mobile.Models.Expenses;
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
        });
    }

    private async void CollectionView_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ExpenseResponse expense)
            return;

        await _viewModel.GoToEditExpenseAsync(expense);

        ((CollectionView)sender).SelectedItem = null;
    }

    private async void Expense_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is not BindableObject bindableObject)
            return;

        if (bindableObject.BindingContext is not ExpenseResponse expense)
            return;

        await _viewModel.GoToEditExpenseAsync(expense);
    }
}
