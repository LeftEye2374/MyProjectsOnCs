using CrabCounterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrabCounterApp.DAL
{
    public class CrabRepository : ICrabRepository
    {
        private readonly AppDbContext _dbContext;


        public CrabRepository(AppDbContext context) => _dbContext = context;

        public async Task<int> GetCountAsync()
        {
            var crab = await _dbContext.CrabCounts.FirstOrDefaultAsync();
            return crab?.Count ?? 0;
        }

        public async Task SaveCountAsync(int count)
        {
            var crab = await _dbContext.CrabCounts.FirstOrDefaultAsync() ?? new CrabCount();
            crab.Count = count;
            if(crab.Id.ToString().Equals("1"))
            {
                _dbContext.CrabCounts.Add(crab);
            }
            else
            {
                _dbContext.CrabCounts.Update(crab);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
