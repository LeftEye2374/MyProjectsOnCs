namespace CrabCounterApp.Core.Services
{
    public interface ICrabService
    {
        Task<int> GetCountAsync();
        Task SaveCountAsync(int count);
    }
}
