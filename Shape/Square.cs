using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Square : IShape
    {
        public double SideLength { get; set; }

        public Square(double sideLength)
        {
            this.SideLength = sideLength;
        }

        public double getArea()
        {
            return SideLength * SideLength;
        }

        public double getHeight()
        {
            return SideLength;
        }

        public double getPerimeter()
        {
            return SideLength * 4;
        }

        public double getWidth()
        {
            return SideLength;
        }
    }
}
