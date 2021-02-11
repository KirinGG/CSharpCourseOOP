using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Triangle : IShape, IComparable
    {
        double sideAbLength;
        double sideBcLength;
        double sideCaLength;

        double x1, x2, x3;
        double y1, y2, y3;

        public double X1
        {
            get
            {
                return x1;
            }
            set
            {
                x1 = value;
                CalculateSidesLength();
            }
        }

        public double X2
        {
            get
            {
                return x2;
            }
            set
            {
                x2 = value;
                CalculateSidesLength();
            }
        }

        public double X3
        {
            get
            {
                return x3;
            }
            set
            {
                x3 = value;
                CalculateSidesLength();
            }
        }

        public double Y1
        {
            get
            {
                return y1;
            }
            set
            {
                y1 = value;
                CalculateSidesLength();
            }
        }

        public double Y2
        {
            get
            {
                return y2;
            }
            set
            {
                y2 = value;
                CalculateSidesLength();
            }
        }

        public double Y3
        {
            get
            {
                return y3;
            }
            set
            {
                y3 = value;
                CalculateSidesLength();
            }
        }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
            this.y1 = y1;
            this.y2 = y2;
            this.y3 = y3;

            CalculateSidesLength();
        }

        public double GetArea()
        {
            double halfPerimeter = this.GetPerimeter() / 2;
            double area = Math.Sqrt(halfPerimeter * (halfPerimeter - sideAbLength) * (halfPerimeter - sideBcLength) * (halfPerimeter - sideCaLength));

            return area;
        }

        public double GetHeight()
        {
            return Max(y1, y2, y3);
        }

        public double GetPerimeter()
        {
            return sideAbLength + sideBcLength + sideCaLength;
        }

        public double GetWidth()
        {
            return Max(x1, x2, x3);
        }

        private void CalculateSidesLength()
        {
            sideAbLength = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            sideBcLength = Math.Sqrt(Math.Pow((x3 - x2), 2) + Math.Pow((y3 - y2), 2));
            sideCaLength = Math.Sqrt(Math.Pow((x1 - x3), 2) + Math.Pow((y1 - y3), 2));
        }

        private double Max(params double[] parameters)
        {
            double max = parameters[0];

            foreach (double parametr in parameters)
            {
                if (parametr > max)
                {
                    max = parametr;
                }
            }
            return max;
        }

        public int CompareTo(object obj)
        {
            IShape shape = obj as IShape;

            if(this.GetArea() > shape.GetArea())
            {
                return 1;
            }

            if(this.GetArea() < shape.GetArea())
            {
                return -1;
            }

            return 0;
        }

        public override string ToString()
        {
            return string.Format("Triangle. AB - {0}, BC - {1}, CA - {2}.", sideAbLength, sideBcLength, sideCaLength);
        }

        public override int GetHashCode()
        {
            return int.MaxValue - base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Triangle triangle = obj as Triangle;

            if (triangle == null)
            {
                return false;
            }

            if (!((this.sideAbLength == triangle.sideAbLength) && (this.sideBcLength == triangle.sideBcLength) && (this.sideCaLength == triangle.sideCaLength)))
            {
                return false;
            }

            return true;
        }
    }
}
