using System;

namespace Vector
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector[] vectors = new Vector[5];

            vectors[0] = new Vector(new double[3] { 1, 2, 3 });
            vectors[1] = new Vector(new double[3] { 3, 2, 1 });
            vectors[2] = new Vector(new double[5] { 1, 2, 3, 4, 5 });
            vectors[3] = new Vector(new double[3] { 5, 2, 3 });
            vectors[4] = new Vector(new double[4] { 7, 2, 3, 4 });

            for(int i = 0; i < vectors.Length; i++)
            {
                Console.WriteLine($"{vectors[i].ToString()}; hash code = {vectors[i].GetHashCode()}");
            }

            Console.WriteLine($"{vectors[0].ToString()} + {vectors[1].ToString()} = {vectors[0].Addition(vectors[1]).ToString()}");
            Console.WriteLine($"{vectors[3].ToString()} - {vectors[2].ToString()} = {vectors[3].Subtraction(vectors[2]).ToString()}");

            double scalar = 3;
            Console.WriteLine($"{vectors[4].ToString()} * {scalar} = {vectors[4].ScalarMultiplication(scalar).ToString()}");
        }
    }
}
