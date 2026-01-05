using Microsoft.EntityFrameworkCore;
using StudApp.AppDbContext;
using StudApp.Models;

namespace StudApp.Mobile.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly SqliteDbContext _context;

        public DocumentService(SqliteDbContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();
        }
        public async Task<Document?> GetDocumentByNameAsync(string name)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.Name == name);
        }

    }
}
