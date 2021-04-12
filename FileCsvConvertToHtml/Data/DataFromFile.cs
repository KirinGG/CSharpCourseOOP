using System;
using System.IO;

namespace FileCsvConvertToHtml.Data
{
    class DataFromFile : IDataProvider, IDisposable
    {
        private StreamReader reader;

        public DataFromFile(string inputFilePath)
        {
            reader = new StreamReader(inputFilePath);
        }

        public string Next()
        {
            return reader.ReadLine();
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
