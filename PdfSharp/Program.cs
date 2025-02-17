using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Создание PDF-документа
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();

        // Получение объекта XGraphics для рисования на странице
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Создание шрифта
        XFont font = new XFont("Arial", 12);

        // Добавление текста с учетом ширины страницы
        string text = "Описание библиотеки PdfSharpCore. PdfSharpCore — это кросс-платформенная версия популярной библиотеки PdfSharp, предназначенная для работы с PDF-документами в приложениях на платформе .NET Core. Она позволяет создавать, читать и изменять PDF-файлы. PdfSharpCore поддерживает простые операции, такие как добавление текста, изображений и графических объектов в PDF-документы.";

        // Получение максимальной ширины для текста
        double pageWidth = page.Width - 40;  // Учитываем отступы (по 20 с каждой стороны)
        double x = 20;  // Отступ слева
        double y = 20;  // Отступ сверху

        // Разделяем текст на строки, чтобы он не выходил за правую часть страницы
        string[] lines = SplitTextIntoLines(text, font, gfx, pageWidth);

        // Рисуем текст
        foreach (var line in lines)
        {
            gfx.DrawString(line, font, XBrushes.Black, new XRect(x, y, pageWidth, 0));
            y += font.GetHeight(); // Переход на новую строку
        }

        // Добавление картинки
        string imagePath = @"D:\Учеба\C# practice in git\Practice_6_semester\PdfSharp\1.jpg";
        if (File.Exists(imagePath))
        {
            XImage image = XImage.FromFile(imagePath);
            
            // Масштабирование изображения, чтобы оно не выходило за пределы страницы
            double imageWidth = image.PixelWidth / 2;
            double imageHeight = image.PixelHeight / 2;
            
            if (imageWidth > pageWidth - 40) // Если изображение слишком широкое
            {
                double scaleFactor = (pageWidth - 40) / imageWidth;
                imageWidth *= scaleFactor;
                imageHeight *= scaleFactor;
            }

            gfx.DrawImage(image, x, y, imageWidth, imageHeight); // Добавление изображения
            y += imageHeight + 10; // Корректируем y для текста после картинки
        }
        else
        {
            Console.WriteLine("Изображение не найдено.");
        }

        // Сохраняем PDF в файл
        string filePath = "document.pdf";
        document.Save(filePath);

        Console.WriteLine($"PDF файл сохранен по пути {filePath}");
    }

    // Функция для разделения текста на строки, чтобы текст не выходил за границы страницы
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

