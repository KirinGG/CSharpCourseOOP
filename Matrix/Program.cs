using System;

namespace IT_Academ_School
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Getting the dimensions of the matrix ---");
            Matrix matrix1 = new Matrix(4, 5);
            Console.WriteLine($" > {matrix1}");
            Console.WriteLine($" result; m - {matrix1.GetLength(0)}; n - {matrix1.GetLength(1)}.");

            Console.WriteLine("--- Transpose ---");
            Matrix matrix2 = new Matrix(new double[2, 2] { { 1, 2 }, { 3, 4 } });
            Console.WriteLine($" > {matrix2}");
            Console.WriteLine($" result: {matrix2.Transpose()}.");

            Console.WriteLine("--- Scalar multiplication ---");
            Matrix matrix3 = new Matrix(new double[2, 2] { { 2, 2 }, { 2, 2 } });
            double scalar = 5;
            Console.WriteLine($" > {matrix3}, scalar - {scalar}");
            Console.WriteLine($" result: {matrix3.ScalarMultiplication(scalar)}.");

            Console.WriteLine("--- Determinant ---");
            Matrix matrix4 = new Matrix(new double[4, 4] { { 2, 3, 4, -3 }, { 2, -6, -4, -1 }, { 0, -3, -4, 0 }, { -6, 3, -6, 2 } });
            Console.WriteLine($" > {matrix4}");
            Console.WriteLine($" result: {matrix4.GetDeterminant()}.");

            Console.WriteLine("--- Vector multiplication ---");
            Vector vector = new Vector(new double[2] { 1, 2 });
            Console.Write($"{ matrix2} * { vector} = ");
            Console.WriteLine(matrix2.GetVectorMultiplication(vector));

            Console.WriteLine("--- Addition ---");
            Console.Write($"{ matrix2} + { matrix3} = ");
            Console.WriteLine(matrix2.Addition(matrix3));
        }
    }
}
