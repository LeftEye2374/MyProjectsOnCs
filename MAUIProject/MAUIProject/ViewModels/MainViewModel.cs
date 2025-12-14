using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUIProject.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "hi, MVVM";

        [RelayCommand]
        private void ChangeText() 
        {
            Message = "New text";
        }
    }
}
