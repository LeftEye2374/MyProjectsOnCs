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
                Debug.WriteLine("Загрузка документов...");
                var docs = await _documentService.GetAllDocumentsAsync();

                Files.Clear();
                foreach (var doc in docs)
                {
                    Files.Add(new DocumentItem
                    {
                        Name = doc.Name,
                        Path = doc.FilePath,
                        Size = $"{doc.FileData.Length / 1024} KB"
                    });
                }
                Debug.WriteLine($"Загружено: {Files.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}");
            }
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task DownloadFile(DocumentItem item)
        {
            try
            {
                var downloadsDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                var fileName = Path.GetFileNameWithoutExtension(item.Path) + ".pdf";
                var targetPath = Path.Combine(downloadsDir, fileName);

                var doc = await _documentService.GetDocumentByNameAsync(item.Name);
                await File.WriteAllBytesAsync(targetPath, doc.FileData);

                await Application.Current.MainPage.DisplayAlert("Успех",
                    $"Скачано: {targetPath}", "OK");
            }
            catch (Exception ex)
            {
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