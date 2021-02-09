using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Square : IShape, IComparable
    {
        public double SideLength { get; set; }

        public Square(double sideLength)
        {
            this.SideLength = sideLength;
        }

        public double GetArea()
        {
            return SideLength * SideLength;
        }

        public double GetHeight()
        {
            return SideLength;
        }

        public double GetPerimeter()
        {
            return SideLength * 4;
        }

        public double GetWidth()
        {
            return SideLength;
        }

        public int CompareTo(object obj)
        {
            IShape shape = obj as IShape;

            if (this.GetArea() > shape.GetArea())
            {
                return 1;
            }

            if (this.GetArea() < shape.GetArea())
            {
                return -1;
            }

            return 0;
        }
    }
}
