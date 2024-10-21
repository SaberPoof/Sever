using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ReportGenerator.Commands
{
    public class GenerateReportCommand : IAsyncCommand
    {
        private readonly IReportGeneratorService _reportGeneratorService;
        private readonly IFileDialogService _fileDialogService;
        private readonly ObservableCollection<Employee> _employees;
        private readonly ObservableCollection<Department> _departments;

        public GenerateReportCommand(IReportGeneratorService reportGeneratorService, IFileDialogService fileDialogService,
                                     ObservableCollection<Employee> employees, ObservableCollection<Department> departments)
        {
            _reportGeneratorService = reportGeneratorService ?? throw new ArgumentNullException(nameof(reportGeneratorService));
            _fileDialogService = fileDialogService ?? throw new ArgumentNullException(nameof(fileDialogService));
            _employees = employees;
            _departments = departments;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _employees != null && _employees.Count > 0 && _departments != null && _departments.Count > 0;
        }

        public async Task ExecuteAsync(object parameter)
        {
            string savePath = _fileDialogService.SaveFile("Word Document|*.docx");
            if (!string.IsNullOrEmpty(savePath))
            {
                try
                {
                    await Task.Run(() => _reportGeneratorService.GenerateReport(_employees, _departments, savePath));
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Не удалось сохранить файл Word. Возможно, файл используется другим процессом.\nОшибка: {ex.Message}", "Ошибка сохранения файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при генерации отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Execute(object parameter)
        {
            ExecuteAsync(parameter).ConfigureAwait(false);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
