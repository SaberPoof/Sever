using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ReportGenerator.Services
{
    public class WordGeneratorService : IReportGeneratorService
    {
        public void GenerateReport(ObservableCollection<Employee> employees, ObservableCollection<Department> departments, string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            GenerateWordReport(employees, departments, filePath);
        }

        private void GenerateWordReport(ObservableCollection<Employee> employees, ObservableCollection<Department> departments, string filePath)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Заголовка 'Отчет по загрузке'
                Paragraph introParagraph = new Paragraph(
                    new Run(
                        new RunProperties(
                            new RunFonts() { Ascii = "Calibri" },
                            new FontSize() { Val = "28" }
                        ),
                        new Text("Отчет по загрузке")
                    )
                )
                {
                    ParagraphProperties = new ParagraphProperties(
                        new Justification() { Val = JustificationValues.Center },
                        new SpacingBetweenLines() { Before = "120", After = "120" } 
                    )
                };
                body.AppendChild(introParagraph);

                // Таблицы для отображения сотрудников и отделов
                Table table = new Table();

                // Настройка стиля таблиц
                TableProperties tblProperties = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                    ),
                    new TableJustification() { Val = TableRowAlignmentValues.Center }
                );
                table.AppendChild(tblProperties);

                // Добавление заголовков таблицы
                TableRow headerRow = new TableRow();
                headerRow.Append(
                    CreateTableCell("Отдел", true, "808080", true, JustificationValues.Center, "4800", "FFFFFF"),
                    CreateTableCell("Количество задач", true, "808080", true, JustificationValues.Center, "4800", "FFFFFF")
                );
                table.AppendChild(headerRow);

                // Добавление данных по отделам и сотрудникам
                foreach (var department in departments)
                {
                    var departmentEmployees = employees
                        .Where(e => e.DepartmentId == department.Id)
                        .OrderByDescending(e => e.TaskCount)
                        .ToList();

                    if (departmentEmployees.Any())
                    {
                        // Добавление строки отдела
                        TableRow deptRow = new TableRow();
                        deptRow.Append(
                            CreateTableCell(department.Name, false, "D9D9D9", true, JustificationValues.Start, "4800", leftIndent: "144"), // Жирный текст для отдела с отступом слева
                            CreateTableCell(departmentEmployees.Sum(e => e.TaskCount).ToString(), false, "D9D9D9", true, JustificationValues.Center, "4800")
                        );
                        table.AppendChild(deptRow);

                        // Добавление строк сотрудников
                        bool alternate = false;
                        foreach (var employee in departmentEmployees)
                        {
                            string backgroundColor = alternate ? "F2F2F2" : "FFFFFF"; // Чередование фона строк

                            // Форматирование Фамилия И.О.
                            string initials = string.Empty;
                            if (!string.IsNullOrWhiteSpace(employee.Name))
                                initials += $"{employee.Name[0]}.";
                            if (!string.IsNullOrWhiteSpace(employee.Patronymic))
                                initials += $"{employee.Patronymic[0]}.";

                            string employeeNameFormatted = $"{employee.Surname} {initials}".Trim(); 
                            TableRow empRow = new TableRow();
                            empRow.Append(
                                CreateTableCell(employeeNameFormatted, false, backgroundColor, false, JustificationValues.Start, "4800", leftIndent: "144"),
                                CreateTableCell(employee.TaskCount.ToString(), false, backgroundColor, false, JustificationValues.Center, "4800")
                            );
                            table.AppendChild(empRow);
                            alternate = !alternate;
                        }
                    }
                }

                body.AppendChild(table);
                mainPart.Document.Save();
            }
        }

        private TableCell CreateTableCell(string text, bool isHeader, string backgroundColor, bool bold, JustificationValues justification, string width, string fontColor = "000000", string leftIndent = "0")
        {
            // Свойства для абзаца
            ParagraphProperties paragraphProperties = new ParagraphProperties(
                new Justification() { Val = justification },
                new SpacingBetweenLines() { Before = "0", After = "0" }, 
                new Indentation() { Left = leftIndent } 
            );

            Run run = new Run(new Text(text));
            RunProperties runProperties = new RunProperties()
            {
                Color = new Color() { Val = fontColor },
                FontSize = new FontSize() { Val = "24" } 
            };
            if (bold)
            {
                runProperties.Bold = new Bold();
            }
            run.RunProperties = runProperties;

            Paragraph paragraph = new Paragraph(paragraphProperties);
            paragraph.Append(run);

            // Создание ячейки и добавляем в нее абзац
            TableCell cell = new TableCell(paragraph)
            {
                TableCellProperties = new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = width },
                    new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = backgroundColor }
                )
            };

            return cell;
        }
    }
}
