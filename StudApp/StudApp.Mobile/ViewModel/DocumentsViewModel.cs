using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudApp.Mobile.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace StudApp.Mobile.ViewModel
{

    public partial class DocumentsViewModel : ObservableObject
    {
        private readonly IDocumentService _documentService;

        [ObservableProperty]
        private ObservableCollection<DocumentItem> files = new();

        public DocumentsViewModel(IDocumentService documentService)
        {
            _documentService = documentService;
            LoadDocumentsAsync();  
        }

        private async void LoadDocumentsAsync()
        {
            try
            {
                Debug.WriteLine("🔄 Загрузка документов из БД...");
                var documents = await _documentService.GetAllDocumentsAsync();

                Debug.WriteLine($"📊 Найдено в БД: {documents.Count} документов");

                Files.Clear();
                foreach (var doc in documents)
                {
                    var item = new DocumentItem
                    {
                        Name = doc.Name,
                        Path = doc.FilePath,
                        Size = $"{doc.FileData.Length / 1024} KB"
                    };
                    Files.Add(item);
                    Debug.WriteLine($"✅ Добавлен: {item.Name}");
                }

                Debug.WriteLine($"✅ Итого в списке: {Files.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Ошибка БД: {ex}");
            }
        }


        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("ViewPage");  
        }

        [RelayCommand]
        private async Task OpenFile(DocumentItem item)
        {
            try
            {
                var filePath = item.Path;

                if (!File.Exists(filePath))
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Файл не найден", "OK");
                    return;
                }

                var result = await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    Title = item.Name,
                    File = new ReadOnlyFile(filePath)
                });

                if (!result)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось открыть файл", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ OpenFile ошибка: {ex}");
                await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }

    public partial class DocumentItem : ObservableObject
    {
        [ObservableProperty] private string name = "";
        [ObservableProperty] private string path = "";
        [ObservableProperty] private string size = "";
    }
}
