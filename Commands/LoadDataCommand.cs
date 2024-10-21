using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ReportGenerator.Commands
{
    public class LoadDataCommand : IAsyncCommand
    {
        private readonly IExcelDataService _excelDataService;
        private readonly IFileDialogService _fileDialogService;
        private readonly Action<ObservableCollection<Employee>, ObservableCollection<Department>, ObservableCollection<TaskModel>> onDataLoaded;

        public LoadDataCommand(
            IExcelDataService excelDataService,
            IFileDialogService fileDialogService,
            Action<ObservableCollection<Employee>, ObservableCollection<Department>, ObservableCollection<TaskModel>> onDataLoaded)
        {
            _excelDataService = excelDataService ?? throw new ArgumentNullException(nameof(excelDataService));
            _fileDialogService = fileDialogService ?? throw new ArgumentNullException(nameof(fileDialogService));
            this.onDataLoaded = onDataLoaded ?? throw new ArgumentNullException(nameof(onDataLoaded));
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async Task ExecuteAsync(object parameter)
        {
            string filePath = _fileDialogService.OpenFile("Excel Files|*.xlsx");
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    // Чтение данных из файла Excel в фоновом потоке
                    var employees = await Task.Run(() => new ObservableCollection<Employee>(_excelDataService.GetEmployees(filePath)));
                    var departments = await Task.Run(() => new ObservableCollection<Department>(_excelDataService.GetDepartments(filePath)));
                    var tasks = await Task.Run(() => new ObservableCollection<TaskModel>(_excelDataService.GetTasks(filePath)));

                    // Обновление данных на основном потоке
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        onDataLoaded?.Invoke(employees, departments, tasks);
                    });
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Не удалось открыть файл Excel. Возможно, файл используется другим процессом.\nОшибка: {ex.Message}", "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузке данных из Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Execute(object parameter)
        {
            ExecuteAsync(parameter).ConfigureAwait(false);
        }
    }
}
