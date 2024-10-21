using ExcelDataReader;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ReportGenerator.Services
{
    /// <summary>
    /// Получение данных из Excel.
    /// </summary>
    public class ExcelDataService : IExcelDataService
    {
        public List<Employee> GetEmployees(string filePath)
        {
            return LoadData(filePath, "Сотрудники", row => new Employee
            {
                Id = long.Parse(row[0].ToString()),
                Surname = row[1].ToString(),
                Name = row[2].ToString(),
                Patronymic = row[3].ToString(),
                DateOfBirth = DateTime.Parse(row[4].ToString()),
                DepartmentId = int.Parse(row[5].ToString())
            });
        }

        public List<Department> GetDepartments(string filePath)
        {
            return LoadData(filePath, "Отделы", row => new Department
            {
                Id = int.Parse(row[0].ToString()),
                Name = row[1].ToString()
            });
        }

        public List<TaskModel> GetTasks(string filePath)
        {
            return LoadData(filePath, "Задачи", row => new TaskModel
            {
                Id = long.Parse(row[0].ToString()),
                EmployeeId = long.Parse(row[1].ToString())
            });
        }

        private List<T> LoadData<T>(string filePath, string tableName, Func<DataRow, T> map)
        {
            var items = new List<T>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[tableName];
                    if (table != null)
                    {
                        for (int i = 1; i < table.Rows.Count; i++) // Пропускаем первую строку (заголовок)
                        {
                            items.Add(map(table.Rows[i]));
                        }
                    }
                }
            }
            return items;
        }
    }
}
