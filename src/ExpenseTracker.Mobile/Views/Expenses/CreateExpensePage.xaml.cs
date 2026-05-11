using ExpenseTracker.Mobile.ViewModels.Expenses;

namespace ExpenseTracker.Mobile.Views.Expenses;

public partial class CreateExpensePage : ContentPage
{
    public CreateExpensePage(CreateExpenseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
