using ReportGenerator.Models;
using System.Collections.ObjectModel;

namespace ReportGenerator.Interfaces
{
    /// <summary>
    /// Интерфейс для формирования отчета.
    /// </summary>
    public interface IReportGeneratorService
    {
        void GenerateReport(ObservableCollection<Employee> employees, ObservableCollection<Department> departments, string filePath);
    }
}
