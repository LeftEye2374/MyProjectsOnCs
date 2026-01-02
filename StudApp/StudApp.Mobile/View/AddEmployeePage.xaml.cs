using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View;

public partial class AddEmployeePage : ContentPage
{
	public AddEmployeePage(AddEmployeeViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}