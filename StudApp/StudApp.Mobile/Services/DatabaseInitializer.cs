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
            var docsDir = Path.Combine(FileSystem.AppDataDirectory, "Documents");
            Directory.CreateDirectory(docsDir);

            var documents = new[]
            {
            new { Name = "Внутренний устав СОООП", AssetPath = "Documents/1.pdf" },
            new { Name = "Договор найма жилого помещения", AssetPath = "Documents/2.pdf" },
            new { Name = "Кодекс корпоративной культуры", AssetPath = "Documents/3.pdf" },
            new { Name = "Положение о студ совете", AssetPath = "Documents/4.pdf" },
            new { Name = "Положение общежитий", AssetPath = "Documents/5.pdf" },
            new { Name = "Правила проживания в общежитии", AssetPath = "Documents/6.pdf" },
            new { Name = "Устав КубГУ", AssetPath = "Documents/7.pdf" },
            new { Name = "Устав СОООП", AssetPath = "Documents/8.pdf" }
        };

            foreach (var doc in documents)
            {
                try
                {
                    var targetPath = Path.Combine(docsDir, Path.GetFileName(doc.AssetPath));

                    using var input = await FileSystem.OpenAppPackageFileAsync(doc.AssetPath);
                    using var output = File.Create(targetPath);
                    var fileData = new byte[input.Length];
                    await input.ReadAsync(fileData, 0, (int)input.Length);

                    _db.Documents.Add(new Document
                    {
                        Name = doc.Name,
                        FilePath = targetPath,
                        FileData = fileData
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Ошибка добавления {doc.Name}");
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
