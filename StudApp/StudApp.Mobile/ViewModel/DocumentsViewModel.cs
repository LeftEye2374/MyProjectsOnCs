using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace StudApp.Mobile.ViewModel
{

    public partial class DocumentsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DocumentItem> files = new();

        public DocumentsViewModel()
        {
            LoadFiles();
        }

        private void LoadFiles()
        {
            Files.Add(new DocumentItem
            {
                Name = "Внутренний устав СОООП",
                Icon = "pdf.png",
                Path = "Documents/1.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Договор найма жилого помещения",
                Icon = "pdf.png",
                Path = "Documents/2.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Кодекс корпоративной культуры",
                Icon = "pdf.png",
                Path = "Documents/3.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Положение о студ совете",
                Icon = "pdf.png",
                Path = "Documents/4.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Положение общежитий",
                Icon = "pdf.png",
                Path = "Documents/5.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Правила проживания в общежитии",
                Icon = "pdf.png",
                Path = "Documents/6.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Устав КубГУ",
                Icon = "pdf.png",
                Path = "Documents/7.pdf"
            });
            Files.Add(new DocumentItem
            {
                Name = "Устав СОООП",
                Icon = "pdf.png",
                Path = "Documents/8.pdf"
            });
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("ViewPage");
        }

        [RelayCommand]
        private async Task OpenFile(DocumentItem file)
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, file.Path);
                await Launcher.Default.OpenAsync(new OpenFileRequest { File = new ReadOnlyFile(filePath) });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось открыть файл: {ex.Message}", "OK");
            }
        }
    }

    public partial class DocumentItem : ObservableObject  
    {
        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string icon = "";

        [ObservableProperty]
        private string path = "";
    }
}
