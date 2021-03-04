using System;
using Shape.Comparer;
using Shape.Shapes;

namespace Shape
{
    class Program
    {
        static void Main(string[] args)
        {
            IShape[] shapes =
            {
                new Square(5),
                new Triangle(1, 1, 2, 2, 2, 1),
                new Circle(7),
                new Square(4),
                new Rectangle(2, 5)
            };

            Array.Sort(shapes, new AreaComparer());
            Console.WriteLine($"The shape with the largest area - {shapes[shapes.Length - 1]}");

            Array.Sort(shapes, new PerimeterComparer());
            Console.WriteLine($"The shape with the second largest perimeter - {shapes[shapes.Length - 2]}");
        }
    }
}
