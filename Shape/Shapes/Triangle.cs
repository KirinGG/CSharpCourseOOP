using System;

namespace Shape.Shapes
{
    class Triangle : IShape
    {
        private double sideABLength;
        private double sideBCLength;
        private double sideCALength;

        private double x1;
        private double x2;
        private double x3;
        private double y1;
        private double y2;
        private double y3;

        public double X1
        {
            get
            {
                return x1;
            }
            set
            {
                x1 = value;
                CalculateSideABLength();
                CalculateSideCALength();
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
                CalculateSideABLength();
                CalculateSideBCLength();
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
                CalculateSideBCLength();
                CalculateSideCALength();
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
                CalculateSideABLength();
                CalculateSideCALength();
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
                CalculateSideABLength();
                CalculateSideBCLength();
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
                CalculateSideBCLength();
                CalculateSideCALength();
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

            CalculateSideABLength();
            CalculateSideBCLength();
            CalculateSideCALength();
        }

        public double GetArea()
        {
            double halfPerimeter = GetPerimeter() / 2;

            return Math.Sqrt(halfPerimeter * (halfPerimeter - sideABLength) * (halfPerimeter - sideBCLength) * (halfPerimeter - sideCALength));
        }

        public double GetHeight()
        {
            return GetMax(y1, y2, y3) - GetMin(y1, y2, y3);
        }

        public double GetPerimeter()
        {
            return sideABLength + sideBCLength + sideCALength;
        }

        public double GetWidth()
        {
            return GetMax(x1, x2, x3) - GetMin(x1, x2, x3);
        }

        private void CalculateSideABLength()
        {
            sideABLength = CalculateSideLength(X1, X2, Y1, Y2);
        }
        private void CalculateSideBCLength()
        {
            sideBCLength = CalculateSideLength(X2, X3, Y2, Y3);
        }
        private void CalculateSideCALength()
        {
            sideCALength = CalculateSideLength(X3, X1, Y3, Y1);
        }

        private static double CalculateSideLength(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private static double GetMax(params double[] parameters)
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

        private static double GetMin(params double[] parameters)
        {
            double min = parameters[0];

            foreach (double parameter in parameters)
            {
                if (parameter < min)
                {
                    min = parameter;
                }
            }

            return min;
        }

        public override string ToString()
        {
            return $"Triangle. Coordinates: {{{x1}, {y1}}}, {{{x2}, {y2}}}, {{{x3}, {y3}}}";
        }

        public override int GetHashCode()
        {
            int prime = 3;
            int hash = 1;

            hash = prime * hash + x1.GetHashCode();
            hash = prime * hash + x2.GetHashCode();
            hash = prime * hash + x3.GetHashCode();
            hash = prime * hash + y1.GetHashCode();
            hash = prime * hash + y2.GetHashCode();
            hash = prime * hash + y3.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            Triangle triangle = (Triangle)obj;

            return (x1 == triangle.x1) && (x2 == triangle.x2) && (x3 == triangle.x3) && (y1 == triangle.y1) && (y2 == triangle.y2) && (y3 == triangle.y3);
        }
    }
}