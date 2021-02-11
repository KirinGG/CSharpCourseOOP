using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Circle : IShape, IComparable
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public double GetHeight()
        {
            return Radius * 2;
        }

        public double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public double GetWidth()
        {
            return Radius * 2;
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
            return string.Format("Circle. Radius - {0}.", Radius);
        }

        public override int GetHashCode()
        {
            return int.MaxValue - base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Circle circle = obj as Circle;

            if(circle == null)
            {
                return false;
            }

            if(this.Radius != circle.Radius)
            {
                return false;
            }

            return true;
        }
    }
}
