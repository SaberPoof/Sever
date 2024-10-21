using System;

namespace ReportGenerator.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public int TaskCount { get; set; }
    }
}
