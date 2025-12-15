
using CrabCounterApp.DAL;

namespace CrabCounterApp.Core.Services
{
    public class CrabService : ICrabService
    {
        private readonly ICrabRepository _repository;

        public CrabService(ICrabRepository repository) => _repository = repository;

        public async Task<int> GetCountAsync() => await _repository.GetCountAsync();

        public async Task SaveCountAsync(int count) => await _repository.SaveCountAsync(count);
        
    }
}
