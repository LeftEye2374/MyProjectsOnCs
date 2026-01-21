using StudApplication.Mobile.ViewModel;

namespace StudApplication.Mobile.View;

public partial class EmployeePage : ContentPage
{
	public EmployeePage(EmployeePageViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}