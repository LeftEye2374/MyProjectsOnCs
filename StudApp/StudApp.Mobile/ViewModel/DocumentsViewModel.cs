using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace StudApp.Mobile.ViewModels;

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
        // Замените на реальные пути к вашим 4 файлам
        Files.Add(new DocumentItem
        {
            Name = "Документ1.pdf",
            Icon = "doctypes_pdf.png",
            Size = "2.3 MB",
            Path = "path/to/document1.pdf"
        });
        Files.Add(new DocumentItem
        {
            Name = "Документ2.docx",
            Icon = "doctypes_docx.png",
            Size = "1.1 MB",
            Path = "path/to/document2.docx"
        });
        Files.Add(new DocumentItem
        {
            Name = "Документ3.xlsx",
            Icon = "doctypes_xlsx.png",
            Size = "450 KB",
            Path = "path/to/document3.xlsx"
        });
        Files.Add(new DocumentItem
        {
            Name = "Документ4.jpg",
            Icon = "doctypes_image.png",
            Size = "1.8 MB",
            Path = "path/to/document4.jpg"
        });
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("ViewPage"); 
    }
}

public class DocumentItem
{
    public string Name { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Size { get; set; } = "";
    public string Path { get; set; } = "";
}
