using Microsoft.Extensions.Logging;
using StudApp.AppDbContext;
using StudApp.Models;

namespace StudApp.Mobile.Services
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly SqliteDbContext _db;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(SqliteDbContext db, ILogger<DatabaseInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _logger.LogInformation("Инициализация БД...");

            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();

            if (!_db.Documents.Any())
            {
                await AddDocumentsAsync();
                _logger.LogInformation("8 документов добавлены в БД!");
            }
            else
            {
                _logger.LogInformation($"В БД уже {_db.Documents.Count()} документов");
            }
        }

        private async Task AddDocumentsAsync()
        {
            var documents = new[]
            {
            new Document { Name = "Внутренний устав СОООП", FilePath = "Documents/1.pdf", FileData = new byte[1024*1024] },
            new Document { Name = "Договор найма жилого помещения", FilePath = "Documents/2.pdf", FileData = new byte[1024*1024*2] },
            new Document { Name = "Кодекс корпоративной культуры", FilePath = "Documents/3.pdf", FileData = new byte[1024*512] },
            new Document { Name = "Положение о студ совете", FilePath = "Documents/4.pdf", FileData = new byte[1024*1024] },
            new Document { Name = "Положение общежитий", FilePath = "Documents/5.pdf", FileData = new byte[1024*768] },
            new Document { Name = "Правила проживания в общежитии", FilePath = "Documents/6.pdf", FileData = new byte[1024*1024*1] },
             new Document { Name = "Устав КубГУ", FilePath = "Documents/7.pdf", FileData = new byte[1024*1024*3] },
            new Document { Name = "Устав СОООП", FilePath = "Documents/8.pdf", FileData = new byte[1024*1024*2] }
            };

            _db.Documents.AddRange(documents);
            await _db.SaveChangesAsync();
        }


    }
}
