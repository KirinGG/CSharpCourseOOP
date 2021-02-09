using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Rectangle : IShape, IComparable
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double getArea()
        {
            return Height * Width;
        }

        public double getHeight()
        {
            return Height;
        }

        public double getPerimeter()
        {
            return (Height + Width) * 2;
        }

        public double getWidth()
        {
            return Width;
        }

        public int CompareTo(object obj)
        {
            IShape shape = obj as IShape;

            if (this.getArea() > shape.getArea())
            {
                return 1;
            }

            if (this.getArea() < shape.getArea())
            {
                return -1;
            }

            return 0;
        }
    }
}
