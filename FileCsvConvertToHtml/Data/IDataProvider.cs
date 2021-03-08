using System;
using System.Collections.Generic;
using System.Text;

namespace FileCsvConvertToHtml
{
    interface IDataProvider
    {
        public string Next();

        public void Close();
    }
}
