using CommunityToolkit.Maui.Views;
using StudApplication.Mobile.Wrapper;

namespace StudApplication.Mobile.Views
{
    public partial class AddEmployeePopup : Popup<bool>
    {
        public AddEmployeePopup(EmployeeWrapper wrapper)
        {
            InitializeComponent();
            BindingContext = wrapper;
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await CloseAsync(false);
        }

        private async void OnSave(object sender, EventArgs e)
        {
            await CloseAsync(true);
        }
    }
}
