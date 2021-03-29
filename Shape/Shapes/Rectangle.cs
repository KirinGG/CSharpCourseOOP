namespace Shape.Shapes
{
    class Rectangle : IShape
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

        public override string ToString()
        {
            return $"Rectangle. Height: {Height}; Width: {Width}.";
        }

        public override int GetHashCode()
        {
            int prime = 37;
            int hash = 1;

            hash = prime * hash + Height.GetHashCode();
            hash = prime * hash + Width.GetHashCode();

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

            Rectangle rectangle = (Rectangle)obj;

            return (Height == rectangle.Height) && (Width == rectangle.Width);
        }
    }
}
