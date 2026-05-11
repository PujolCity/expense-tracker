using ExpenseTracker.Mobile.Views.Expenses;

namespace ExpenseTracker.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(CreateExpensePage), typeof(CreateExpensePage));
    }
}
