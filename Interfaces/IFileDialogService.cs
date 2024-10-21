namespace ReportGenerator.Interfaces
{
    /// <summary>
    /// Фильтрация расширений файлов.
    /// </summary>
    public interface IFileDialogService
    {
        string OpenFile(string filter);
        string SaveFile(string filter);
    }
}
