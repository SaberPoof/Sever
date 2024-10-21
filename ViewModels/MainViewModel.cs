using ReportGenerator.Commands;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ReportGenerator.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IExcelDataService _excelDataService;
        private readonly IReportGeneratorService _reportGeneratorService;
        private readonly IFileDialogService _fileDialogService;

        public ObservableCollection<Employee> Employees { get; private set; }
        public ObservableCollection<Department> Departments { get; private set; }
        public ObservableCollection<TaskModel> Tasks { get; private set; }

        public IAsyncCommand LoadDataCommand { get; }
        public GenerateReportCommand GenerateReportCommand { get; }

        public MainViewModel(IExcelDataService excelDataService, IReportGeneratorService reportGeneratorService, IFileDialogService fileDialogService)
        {
            _excelDataService = excelDataService;
            _reportGeneratorService = reportGeneratorService;
            _fileDialogService = fileDialogService;

            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            Tasks = new ObservableCollection<TaskModel>();

            LoadDataCommand = new LoadDataCommand(_excelDataService, _fileDialogService, OnDataLoaded);
            GenerateReportCommand = new GenerateReportCommand(_reportGeneratorService, _fileDialogService, Employees, Departments);

            // Подписка на события изменения коллекций, чтобы обновлять состояние команды
            Employees.CollectionChanged += (s, e) => GenerateReportCommand.RaiseCanExecuteChanged();
            Departments.CollectionChanged += (s, e) => GenerateReportCommand.RaiseCanExecuteChanged();
        }

        private void OnDataLoaded(ObservableCollection<Employee> employees, ObservableCollection<Department> departments, ObservableCollection<TaskModel> tasks)
        {
            // Обновляем коллекции
            CollectionUpdaterService.UpdateCollection(Employees, employees);
            CollectionUpdaterService.UpdateCollection(Departments, departments);
            CollectionUpdaterService.UpdateCollection(Tasks, tasks);

            // Обновляем TaskCount для каждого сотрудника
            UpdateTaskCounts();

            // Обновляем доступность команды GenerateReport
            GenerateReportCommand.RaiseCanExecuteChanged();
        }

        private void UpdateTaskCounts()
        {
            foreach (var employee in Employees)
            {
                employee.TaskCount = Tasks.Count(t => t.EmployeeId == employee.Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class CollectionUpdaterService
    {
        public static void UpdateCollection<T>(ObservableCollection<T> targetCollection, ObservableCollection<T> sourceCollection)
        {
            targetCollection.Clear();
            foreach (var item in sourceCollection)
            {
                targetCollection.Add(item);
            }
        }
    }
}
