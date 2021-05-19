using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Tree
{
    class IntComparer : IComparer<int>
    {
        public int Compare([AllowNull] int x, [AllowNull] int y)
        {
            return x.CompareTo(y);
        }
    }
}
