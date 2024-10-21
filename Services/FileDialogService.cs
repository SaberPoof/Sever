using Microsoft.Win32;
using ReportGenerator.Interfaces;

namespace ReportGenerator.Services
{
    /// <summary>
    /// Работа с файломи.
    /// </summary>
    public class FileDialogService : IFileDialogService
    {
        public string OpenFile(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : string.Empty;
        }

        public string SaveFile(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = filter };
            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : string.Empty;
        }
    }
}
