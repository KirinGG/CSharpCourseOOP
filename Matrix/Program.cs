using System;

namespace IT_Academ_School
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix1 = new Matrix(5, 5);
            Matrix matrix2 = new Matrix(new double[2, 2] { { 1, 2 }, { 3, 4 } });
            Matrix matrix3 = new Matrix(new double[3, 3] { { 2, 4, 0 }, { -2, 1, 3 }, { -1, 0, 1 } });
            Matrix matrix4 = new Matrix(new double[2, 2] { { 2, 2 }, { 2, 2 } });
            Vector.Vector vector = new Vector.Vector(new double[3] { 1, 2, -1 });
            Console.WriteLine(matrix1);
            Console.WriteLine(matrix2);
            Console.WriteLine(matrix2.Transpose());
            Console.WriteLine(matrix2.ScalarMultiplication(5));
            Console.WriteLine(matrix3.VectorMultiplication(vector));
            Console.WriteLine("--- Addition ---");
            Console.WriteLine($"{matrix2} + {matrix4} = {matrix2.Addition(matrix4)}");

            Console.WriteLine("--- Multiplication ---");
            Matrix matrix5 = Matrix.MatrixMultiplication(new Matrix(new double[2, 3] { { 1, 2, 1 }, { 0, 1, 2 } }), new Matrix(new double[3, 2] { { 1, 0 }, { 0, 1 }, { 1, 1 } }));
            Console.WriteLine(matrix5);
            Matrix matrix6 = Matrix.MatrixMultiplication(new Matrix(new double[3, 2] { { 1, 0 }, { 0, 1 }, { 1, 1 } }), new Matrix(new double[2, 3] { { 1, 2, 1 }, { 0, 1, 2 } }));
            Console.WriteLine(matrix6);
        }
    }
}
