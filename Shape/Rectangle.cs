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

        public override string ToString()
        {
            return string.Format("Rectangle. Height - {0}; Width - {1}.", Height, Width);
        }

        public override int GetHashCode()
        {
            return int.MaxValue - base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Rectangle rectangle = obj as Rectangle;

            if (rectangle == null)
            {
                return false;
            }

            if (!((this.Height == rectangle.Height) && (this.Width == rectangle.Width)))
            {
                return false;
            }

            return true;
        }
    }
}
