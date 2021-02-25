using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace IT_Academ_School
{
    class PerimetrComparer : IComparer<IShape>
    {
        public int Compare([AllowNull] IShape x, [AllowNull] IShape y)
        {
            return x.GetPerimeter().CompareTo(y.GetPerimeter());
        }
    }
}
