using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Shape
{
    class PerimetrComparer : IComparer<IShape>
    {
        public int Compare([AllowNull] IShape x, [AllowNull] IShape y)
        {
            if(x.GetPerimeter() > y.GetPerimeter())
            {
                return 1;
            }

            if(x.GetPerimeter() < y.GetPerimeter())
            {
                return -1;
            }

            return 0;
        }
    }
}
