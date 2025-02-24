using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        PdfDocument document = new PdfDocument(); //Создаем PDF документ
        PdfPage page = document.AddPage();

        XGraphics gfx = XGraphics.FromPdfPage(page); //Получаем объект XGraphics для рисования на странице

        XFont font = new XFont("Arial", 12);

        string text = "Описание библиотеки PdfSharpCore. PdfSharpCore — это кросс-платформенная версия популярной библиотеки PdfSharp, предназначенная для работы с PDF-документами в приложениях на платформе .NET Core. Она позволяет создавать, читать и изменять PDF-файлы. PdfSharpCore поддерживает простые операции, такие как добавление текста, изображений и графических объектов в PDF-документы.";

        double pageWidth = page.Width - 40;  //Учитываем отступы (по 20 с каждой стороны)
        double x = 20;  //Отступ слева
        double y = 20;  //Отступ сверху

        string[] lines = SplitTextIntoLines(text, font, gfx, pageWidth);

        foreach (var line in lines)
        {
            gfx.DrawString(line, font, XBrushes.Black, new XRect(x, y, pageWidth, 0));
            y += font.GetHeight(); 
        }

        string imagePath = @"D:\Учеба\C# practice in git\Practice_6_semester\PdfSharp\1.jpg"; //Добавление картинки
        if (File.Exists(imagePath))
        {
            XImage image = XImage.FromFile(imagePath);
            
            //Масштабирование изображения, чтобы оно не выходило за пределы страницы
            double imageWidth = image.PixelWidth / 2;
            double imageHeight = image.PixelHeight / 2;
            
            if (imageWidth > pageWidth - 40) 
            {
                double scaleFactor = (pageWidth - 40) / imageWidth;
                imageWidth *= scaleFactor;
                imageHeight *= scaleFactor;
            }

            gfx.DrawImage(image, x, y, imageWidth, imageHeight); // Добавление изображения
            y += imageHeight + 10; 
        }
        else
        {
            Console.WriteLine("Изображение не найдено.");
        }
        string filePath = "document.pdf";
        document.Save(filePath);

        Console.WriteLine($"PDF файл сохранен по пути {filePath}");
    }
    static string[] SplitTextIntoLines(string text, XFont font, XGraphics gfx, double pageWidth)
    {
        var lines = new System.Collections.Generic.List<string>();
        string[] words = text.Split(' ');

        string currentLine = "";
        foreach (var word in words)
        {
            string testLine = currentLine + (currentLine == "" ? "" : " ") + word;
            if (gfx.MeasureString(testLine, font).Width <= pageWidth)
            {
                currentLine = testLine;
            }
            else
            {
                lines.Add(currentLine);
                currentLine = word;
            }
        }

        if (currentLine.Length > 0)
            lines.Add(currentLine);

        return lines.ToArray();
    }
}