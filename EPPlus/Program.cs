using ClosedXML.Excel;

class Program
{
    static void Main()
    {
        var workbook = new XLWorkbook(); //Создание нового рабочего файла Excel
        var worksheet = workbook.AddWorksheet("Sheet1");

        worksheet.Cell(1, 1).Value = "Первая пара";
        worksheet.Cell(1, 2).Value = "Вторая пара";
        worksheet.Cell(2, 1).Value = "Апр";
        worksheet.Cell(2, 2).Value = "АИС";

        // Сохранение в файл
        workbook.SaveAs("example.xlsx");
    }
}
