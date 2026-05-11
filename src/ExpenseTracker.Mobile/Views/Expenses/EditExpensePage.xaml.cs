using ExpenseTracker.Mobile.ViewModels.Expenses;

namespace ExpenseTracker.Mobile.Views.Expenses;

public partial class EditExpensePage : ContentPage
{
    public EditExpensePage(EditExpenseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
