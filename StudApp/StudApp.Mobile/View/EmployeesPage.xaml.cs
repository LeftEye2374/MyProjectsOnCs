using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View
{

    public partial class EmployeesPage : ContentPage
    {
        public EmployeesPage(EmployeesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Loaded += async (s, e) => await viewModel.AddEmployeeCommand.ExecuteAsync(null);
        }
    }
}