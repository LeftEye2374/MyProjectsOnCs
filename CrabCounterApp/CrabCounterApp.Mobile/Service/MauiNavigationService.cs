using CrabCounterApp.Core.Services;

namespace CrabCounterApp.Mobile.Service
{
    internal class MauiNavigationService : INavigationService
    {
        public async Task NavigateToAsync(string route)
        {
            if (Shell.Current != null)
                await Shell.Current.GoToAsync(route);
        }
    }
}
