namespace Shape.Shapes
{
    class Square : IShape
    {
        public double SideLength { get; set; }

        public Square(double sideLength)
        {
            SideLength = sideLength;
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

        public override string ToString()
        {
            return $"Square. Side length: {SideLength}.";
        }

        public override int GetHashCode()
        {
            return SideLength.GetHashCode();
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

            Square square = obj as Square;

            return SideLength == square.SideLength;    
        }
    }
}
