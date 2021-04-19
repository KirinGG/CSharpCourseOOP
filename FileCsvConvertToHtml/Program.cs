using System;
using System.IO;

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

            using StreamReader reader = new StreamReader(inputFilePath);
            using StreamWriter writer = new StreamWriter(outFilePath);

            Converter converter = new Converter(reader, writer);
            converter.ConvertCSVToHTML();          
         }
    }
}
