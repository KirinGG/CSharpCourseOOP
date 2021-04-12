using System;
using System.IO;

namespace FileCsvConvertToHtml.Data
{
    class DataToFile : IDataReceiver, IDisposable
    {
        private StreamWriter writer;

        public DataToFile(string outFilePath)
        {
            writer = new StreamWriter(outFilePath);
        }

        public void Append(string data)
        {
            writer.Write(data);
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
