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

        public double GetArea()
        {
            return Height * Width;
        }

        public double GetHeight()
        {
            return Height;
        }

        public double GetPerimeter()
        {
            return (Height + Width) * 2;
        }

        public double GetWidth()
        {
            return Width;
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
