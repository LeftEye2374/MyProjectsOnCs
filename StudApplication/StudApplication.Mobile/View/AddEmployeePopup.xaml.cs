using StudApplication.Mobile.Wrapper;

namespace StudApplication.Mobile.View;

public partial class AddEmployeePopup : ContentPage
{
    public AddEmployeePopup(EmployeeWrapper wrapper)
    {
        InitializeComponent();
        BindingContext = wrapper;
    }

    private void OnCancel(object sender, EventArgs e)
    {
        Close(false);
    }

    private void OnSave(object sender, EventArgs e)
    {
        Close(true);
    }
}