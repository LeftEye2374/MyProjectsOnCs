namespace CrabCounterApp.DAL
{
    public interface ICrabRepository
    {
        Task<int> GetCountAsync();
        Task SaveCountAsync(int count);
    }
}
