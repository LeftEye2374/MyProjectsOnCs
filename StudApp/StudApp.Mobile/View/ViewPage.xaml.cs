using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View;

public partial class ViewPage : ContentPage
{
	public ViewPage(ViewViewModel model)
	{
		InitializeComponent();
		BindingContext = model;	
	}
}