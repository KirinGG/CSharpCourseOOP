using System;
using System.IO;

namespace FileCsvConvertToHtml
{
    class DataToFile : IDataReceiver
    {
        private string outFilePath;
        StreamWriter writer;

        public DataToFile(string outFilePath)
        {
            this.outFilePath = outFilePath;

            try
            {
                writer = new StreamWriter(outFilePath);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }

        public void Append(string data)
        {
            writer.Write(data);
        }

        public void Close()
        {
            writer.Close();
        }
    }
}
