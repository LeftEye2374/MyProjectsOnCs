using CommunityToolkit.Maui.Views;
using StudApp.Mobile.Wrappers;

namespace StudApp.Mobile.View
{

    public partial class AddEmployeePopup : Popup
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
}