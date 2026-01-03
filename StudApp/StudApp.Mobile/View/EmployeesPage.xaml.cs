using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View
{
    public partial class EmployeesPage : ContentPage
    {
        private readonly EmployeesViewModel _viewModel;

        public EmployeesPage(EmployeesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadEmployeesCommand.ExecuteAsync(null);
        }
    }
}
