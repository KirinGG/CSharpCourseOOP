using System;

namespace Vectors
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector[] vectors = new Vector[5];
            
            vectors[0] = new Vector(new double[4] { 1, 2, 3, 4 });
            vectors[1] = new Vector(new double[3] { 3, 2, 1 });
            vectors[2] = new Vector(new double[5] { 1, 2, 3, 4, 5 });
            vectors[3] = new Vector(new double[3] { 5, 2, 3 });
            vectors[4] = new Vector(new double[4] { 7, 2, 3, 4 });
 
            for (int i = 0; i < vectors.Length; i++)
            {
                Console.WriteLine($"{vectors[i]}; hash code = {vectors[i].GetHashCode()}");
            }

            Console.Write($"{vectors[0]} + {vectors[1]}");
            Console.WriteLine($" = {vectors[0].Add(vectors[1])}");
            Console.Write($"{vectors[3]} - {vectors[2]}");
            Console.WriteLine($" = {vectors[3].Subtract(vectors[2])}");

            double scalar = 3;
            Console.WriteLine($"{vectors[4]} * {scalar} = {vectors[4].MultiplyByScalar(scalar)}");
        }
    }
}
