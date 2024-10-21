using ReportGenerator.Models;
using System.Collections.Generic;

namespace ReportGenerator.Interfaces
{
    /// <summary>
    /// Интерфейс для ипорта моделей из Excel.
    /// </summary>
    public interface IExcelDataService
    {
        List<Employee> GetEmployees(string filePath);
        List<Department> GetDepartments(string filePath);
        List<TaskModel> GetTasks(string filePath);
    }
}
