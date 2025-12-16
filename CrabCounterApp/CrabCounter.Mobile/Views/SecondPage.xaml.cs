using CrabCounter.Mobile.ViewModels;

namespace CrabCounter.Mobile.Views;

public partial class SecondPage : ContentPage
{
	public SecondPage()
	{
		InitializeComponent();
		BindingContext = new SecondPageViewModel();
	}
}