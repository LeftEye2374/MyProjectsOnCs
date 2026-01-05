using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View;

public partial class DocumentsPage : ContentPage
{
	public DocumentsPage(DocumentsViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}