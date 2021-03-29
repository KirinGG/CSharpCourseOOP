using System;

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
            
            DataFromFile dataFromFile = new DataFromFile(inputFilePath);
            DataToFile dataToFile = new DataToFile(outFilePath);

            HtmlDocument htmlDocument = new HtmlDocument(dataFromFile, dataToFile);
            htmlDocument.CreateHeader();
            htmlDocument.CreateBody();
            htmlDocument.CreateFooter();
            htmlDocument.Close();
         }
    }
}
