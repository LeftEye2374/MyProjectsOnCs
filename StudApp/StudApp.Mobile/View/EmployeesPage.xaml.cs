using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View;

public partial class EmployeesPage : ContentPage
{
	public EmployeesPage(EmployeesViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}