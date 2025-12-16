using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrabCounter.SqliteDbContext;

namespace CrabCounter.Mobile.ViewModels
{
    public partial class SecondPageViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private string count;


        [RelayCommand]
        private void increment()
        {
            //count = finalCount++;
            //return count;
        }

        [RelayCommand]
        private void decrement()
        {
            //count = finalCount--;
            //return count;
        }

        [RelayCommand]
        private void save()
        {

        }


        private int getCount()
        {
            //_context = 

            return 1;
        }


    }
}
