using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUIProject.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private int passwordLegth = 8;

        [ObservableProperty]
        private string finalResult = "Password will created here";

        [RelayCommand]
        private void GeneratePasswordCommand()
        {
            const string chars = "ABCDEFGHJIUOMabcdefg0123456789";
            Random random = new Random();
            finalResult = new string(Enumerable.Repeat(chars, passwordLegth).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
