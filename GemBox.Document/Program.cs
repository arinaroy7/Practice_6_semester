using GemBox.Document;

class Program
{
    static void Main()
    {
        ComponentInfo.SetLicense("FREE-LIMITED-KEY"); 

        DocumentModel document = new DocumentModel();

        Paragraph header = new Paragraph(document, "Библиотека GemBox.Document"); //Создаем параграф с заголовком и выравниваем его по центру
        header.ParagraphFormat.Alignment = HorizontalAlignment.Center;  

        document.Sections.Add(new Section(document, header)); //Добавляем параграф в документ
        
        document.Save("report.docx");
    }
}