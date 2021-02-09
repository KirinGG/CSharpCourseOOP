using System;

namespace Shape
{
    class Program
    {
        static void Main(string[] args)
        {
            IShape[] shapes = new IShape[5];
            
            shapes[0] = new Square(5);
            shapes[1] = new Square(3);
            shapes[2] = new Square(8);
            shapes[3] = new Square(1);
            shapes[4] = new Square(2);

            Array.Sort(shapes);

            foreach(IShape shape in shapes)
            {
                Console.WriteLine(shape.getArea());
            }

            Console.WriteLine(shapes[0].getPerimeter());
        }
    }
}
