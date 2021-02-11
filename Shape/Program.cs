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
            shapes[2] = new Square(5);
            shapes[3] = new Square(1);
            shapes[4] = new Square(2);
       
            Console.WriteLine("--- Equals ---");

            if (shapes[0].Equals(shapes[2]))
            {
                Console.WriteLine("{0} = {1}", shapes[0].ToString(), shapes[2].ToString());
            }
            else
            {
                Console.WriteLine("{0} <> {1}", shapes[0].ToString(), shapes[2].ToString());
            }

            Console.WriteLine("--- Sort by area ---");
            Array.Sort(shapes);

            foreach (IShape shape in shapes)
            {
                Console.WriteLine(shape.GetArea());
            }

            Console.WriteLine("--- Sort by perimeter ---");
            Array.Sort(shapes, new PerimetrComparer());

            foreach (IShape shape in shapes)
            {
                Console.WriteLine(shape.GetPerimeter());
            }
        }
    }
}
