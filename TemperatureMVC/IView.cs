using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temperature
{
    interface IView
    {
        void RefreshView(double temperature);

        void SetFrom(string measurementUnit);

        void SetTo(string measurementUnit);

        void SetValue(double temperature);
    }
}
