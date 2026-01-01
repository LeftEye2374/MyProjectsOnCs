using CommunityToolkit.Maui.Views;
using StudApp.Mobile.Wrappers;

namespace StudApp.Mobile.View;

public partial class AddEmployeePopupPage : Popup
{
	public AddEmployeePopupPage(EmployeeWrapper wrapper)
	{
		InitializeComponent();
		BindingContext = wrapper;
	}
}