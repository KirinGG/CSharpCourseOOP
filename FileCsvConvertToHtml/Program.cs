using System;
using System.IO;
using FileCsvConvertToHtml.Data;

namespace FileCsvConvertToHtml
{
    class FileCsvConvertToHtml
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Необходимо указать параметры: [имя входного файла.csv] [имя выходного файла.html]");
                return;
            }

            string inputFilePath = args[0];
            string outFilePath = args[1];

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Входной файл: {inputFilePath} не найден!") ;
                return;
            }

            if (!File.Exists(outFilePath))
            {
                Console.WriteLine($"Выходной файл: {outFilePath} не найден!");
                return;
            }

            using DataFromFile dataFromFile = new DataFromFile(inputFilePath);
            using DataToFile dataToFile = new DataToFile(outFilePath);

            HtmlDocument htmlDocument = new HtmlDocument(dataFromFile, dataToFile);
            htmlDocument.CreateHeader();
            htmlDocument.CreateBody();
            htmlDocument.CreateFooter();
         }
    }
}
