using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Shape.Shapes;

namespace Shape.Comparer
{
    class PerimeterComparer : IComparer<IShape>
    {
        public int Compare([AllowNull] IShape shape1, [AllowNull] IShape shape2)
        {
            return shape1.GetPerimeter().CompareTo(shape2.GetPerimeter());
        }
    }
}
