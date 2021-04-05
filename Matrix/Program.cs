using System;
using Vectors;

namespace Matrixes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Getting the dimensions of the matrix ---");
            Matrix matrix1 = new Matrix(4, 5);
            Console.WriteLine($" > {matrix1}");

            Console.WriteLine("--- Transpose ---");
            Matrix matrix2 = new Matrix(new double[2, 2] { { 1, 2 }, { 3, 4 } });
            Console.WriteLine($" > {matrix2}");
            Console.WriteLine($" result: {matrix2.Transpose()}.");

            Console.WriteLine("--- Scalar multiplication ---");
            Matrix matrix3 = new Matrix(new double[2, 2] { { 2, 2 }, { 2, 2 } });
            double scalar = 5;
            Console.WriteLine($" > {matrix3}, scalar - {scalar}");
            Console.WriteLine($" result: {matrix3.MultiplyByScalar(scalar)}.");

            Console.WriteLine("--- Determinant test 1---");
            Matrix matrix4 = new Matrix(new double[4, 4] { { 2, 3, 4, -3 }, { 2, -6, -4, -1 }, { 0, -3, -4, 0 }, { -6, 3, -6, 2 } });
            Console.WriteLine($" > {matrix4}");
            Console.WriteLine($" result: {matrix4.GetDeterminant()}.");

            Console.WriteLine("--- Determinant test 2---");
            Matrix matrix5 = new Matrix(new double[3, 3] { { 2, 0, 0 }, { 0, 3, 0 }, { 0, 0, 5 } });
            Console.WriteLine($" > {matrix5}");
            Console.WriteLine($" result: {matrix5.GetDeterminant()}.");

            Console.WriteLine("--- Vector multiplication ---");
            Vector vector = new Vector(new double[3] { 1, 2, 3 });
            Console.Write($"{ matrix5} * { vector} = ");
            Console.WriteLine(matrix5.MultiplyByVector(vector));

            Console.WriteLine("--- Addition ---");
            Console.Write($"{ matrix2} + { matrix3} = ");
            Console.WriteLine(matrix2.Add(matrix3));

            Console.WriteLine("--- Subtraction ---");
            Console.Write($"{ matrix2} - { matrix3} = ");
            Console.WriteLine(matrix2.Subtract(matrix3));
        }
    }
}
