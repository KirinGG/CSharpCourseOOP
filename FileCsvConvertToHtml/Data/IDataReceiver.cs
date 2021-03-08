using System;
using System.Collections.Generic;
using System.Text;

namespace FileCsvConvertToHtml
{
    interface IDataReceiver
    {
        public void Append(string data);

        public void Close();
    }
}
