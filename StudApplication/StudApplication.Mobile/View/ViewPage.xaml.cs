using StudApplication.Mobile.ViewModel;

namespace StudApplication.Mobile.View;

public partial class ViewPage : ContentPage
{
	public ViewPage(ViewPageViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}