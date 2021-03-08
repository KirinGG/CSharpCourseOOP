using System;
using System.IO;

namespace FileCsvConvertToHtml
{
    class DataFromFile : IDataProvider
    {
        private string inputFilePath;
        StreamReader reader;

        public DataFromFile(string inputFilePath)
        {
            this.inputFilePath = inputFilePath;

            try
            {
                reader = new StreamReader(inputFilePath);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }

        public string Next()
        {
            return reader.ReadLine();
        }

        public void Close()
        {
            reader.Close();
        }
    }
}
