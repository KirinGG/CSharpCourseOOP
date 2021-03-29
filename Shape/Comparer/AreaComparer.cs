using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Shape.Shapes;

namespace Shape
{
    class AreaComparer : IComparer<IShape>
    {
        public int Compare([AllowNull] IShape shape1, [AllowNull] IShape shape2)
        {
            return shape1.GetArea().CompareTo(shape2.GetArea());
        }
    }
}
