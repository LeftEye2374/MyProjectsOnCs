using StudApp.Models;

namespace StudApp.Mobile.Services
{
    public interface IDocumentService
    {
        Task<List<Document>> GetAllDocumentsAsync();
    }
}
