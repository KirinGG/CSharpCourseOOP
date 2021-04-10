using System;

namespace FileCsvConvertToHtml.Data
{
    class DataToDisplay : IDataReceiver
    {
        public void Append(string data)
        {
            Console.Write(data);
        }
    }
}
