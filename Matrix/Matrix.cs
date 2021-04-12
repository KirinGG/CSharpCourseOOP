using System;
using Vectors;

namespace Matrixes
{
    public class Matrix
    {
        private Vector[] rows;

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1)
            {
                throw new ArgumentException("The number of rows must be greater than zero!", nameof(rowsCount));
            }

            if (columnsCount < 1)
            {
                throw new ArgumentException("The number of columns must be greater than zero!", nameof(columnsCount));
            }

            rows = new Vector[rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                rows[i] = new Vector(columnsCount);
            }
        }

        public Matrix(Matrix matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentException("The matrix cannot be empty!", nameof(matrix));
            }

            rows = new Vector[matrix.rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = matrix.GetRow(i);
            }
        }

        public Matrix(double[,] components)
        {
            if (components == null || components.GetLength(0) == 0 || components.GetLength(1) == 0)
            {
                throw new ArgumentException("The array cannot be empty!", nameof(components));
            }

            rows = new Vector[components.GetLength(0)];

            for (int i = 0; i < rows.Length; i++)
            {
                double[] row = new double[components.GetLength(1)];

                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = components[i, j];
                }

                rows[i] = new Vector(row);
            }
        }

        public Matrix(Vector[] vectors)
        {
            if (vectors == null || vectors.Length == 0)
            {
                throw new ArgumentException("The vector cannot be empty!", nameof(vectors));
            }

            var maxColumnsCount = GetMaxSize(vectors);
            rows = new Vector[vectors.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                double[] row = new double[vectors[i].GetSize()];

                for (int j = 0; j < vectors[i].GetSize(); j++)
                {
                    row[j] = vectors[i].GetComponent(j);
                }

                rows[i] = new Vector(maxColumnsCount, row);
            }
        }

        private static int GetMaxSize(Vector[] vectors)
        {
            int maxSize = vectors[0].GetSize();

            for (int i = 1; i < vectors.Length; i++)
            {
                if (maxSize < vectors[i].GetSize())
                {
                    maxSize = vectors[i].GetSize();
                }
            }

            return maxSize;
        }

        public Vector GetRow(int rowIndex)
        {
            CheckIndex(rowIndex, rows.Length, nameof(rowIndex));

            return new Vector(rows[rowIndex]);
        }

        public void SetRow(int rowIndex, Vector vector)
        {
            CheckIndex(rowIndex, rows.Length, nameof(rowIndex));

            if (vector.GetSize() != rows[0].GetSize())
            {
                throw new ArgumentException(nameof(vector), $"invalid vector size. Vector size: {vector.GetSize()}, number of matrix columns: {rows[0].GetSize()}");
            }

            rows[rowIndex] = new Vector(vector);
        }

        public Vector GetColumn(int columnIndex)
        {
            CheckIndex(columnIndex, rows[0].GetSize(), nameof(columnIndex));

            double[] column = new double[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                column[i] = rows[i].GetComponent(columnIndex);
            }

            return new Vector(column);
        }

        public Matrix Transpose()
        {
            Vector[] transposedRows = new Vector[rows[0].GetSize()];

            for (int i = 0; i < rows[0].GetSize(); i++)
            {
                transposedRows[i] = GetColumn(i);
            }

            rows = transposedRows;

            return this;
        }

        public Matrix MultiplyByScalar(double scalar)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].MultiplyByScalar(scalar);
            }

            return this;
        }

        public double GetDeterminant()
        {
            return CalculateDeterminant(this);
        }

        private static double CalculateDeterminant(Matrix matrix)
        {
            if (matrix.rows.Length != matrix.rows[0].GetSize())
            {
                throw new InvalidOperationException($"The matrix must be square! Rows: {matrix.rows.Length}, columns: {matrix.rows[0].GetSize()}.");
            }

            if (matrix.rows.Length == 1)
            {
                return matrix.rows[0].GetComponent(0);
            }

            if (matrix.rows.Length == 2)
            {
                return matrix.rows[0].GetComponent(0) * matrix.rows[1].GetComponent(1) - matrix.rows[0].GetComponent(1) * matrix.rows[1].GetComponent(0);
            }

            double result = 0;
            int i = 0;

            for (int j = 0; j < matrix.rows.Length; j++)
            {
                Matrix minor = GetMinor(matrix, i, j);
                result += matrix.rows[0].GetComponent(j) * Math.Pow(-1, (i + 1) + (j + 1)) * CalculateDeterminant(minor);
            }

            return result;
        }

        private static Matrix GetMinor(Matrix matrix, int rowIndex, int columnIndex)
        {
            double[,] array = new double[matrix.rows.Length - 1, matrix.rows[0].GetSize() - 1];
            int arrayRowIndex = 0;

            for (int i = 0; i < matrix.rows.Length; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }

                int arrayColumnIndex = 0;

                for (int j = 0; j < matrix.rows[0].GetSize(); j++)
                {
                    if (j == columnIndex)
                    {
                        continue;
                    }

                    array[arrayRowIndex, arrayColumnIndex] = matrix.rows[i].GetComponent(j);
                    arrayColumnIndex++;
                }

                arrayRowIndex++;
            }

            return new Matrix(array);
        }

        public Vector MultiplyByVector(Vector vector)
        {
            if (rows[0].GetSize() != vector.GetSize())
            {
                throw new ArgumentException($"The number of columns of the matrix must be equal to the number of elements of the vector! Matrix columns:{rows[0].GetSize()}, vector length:{vector.GetSize()}", nameof(vector));
            }

            double[] array = new double[vector.GetSize()];

            for (int i = 0; i < rows.Length; i++)
            {
                array[i] = Vector.GetScalarProduct(rows[i], vector);
            }

            return new Vector(array);
        }

        public Matrix Add(Matrix matrix)
        {
            CheckSize(matrix, this);

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].Add(matrix.GetRow(i));
            }

            return this;
        }

        public Matrix Subtract(Matrix matrix)
        {
            CheckSize(matrix, this);

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].Subtract(matrix.GetRow(i));
            }

            return this;
        }

        public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
        {
            CheckSize(matrix1, matrix2);

            return matrix1.Add(matrix2);
        }

        public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
        {
            CheckSize(matrix1, matrix2);

            return matrix1.Subtract(matrix2);
        }

        public static Matrix GetProduct(Matrix matrix1, Matrix matrix2)
        {
            if(matrix1 == null || matrix2 == null)
            {
                throw new InvalidOperationException("!he matrix cannot be null");
            }

            if (matrix1.rows.Length != matrix2.rows[0].GetSize())
            {
                throw new ArgumentException($"The number of rows in the 1st matrix must be equal to the number of columns in the second matrix! Matix1 rows: {matrix1.rows.Length}, matrix2 columns:{matrix2.rows[0].GetSize()}");
            }

            double[,] components = new double[matrix1.rows.Length, matrix2.rows[0].GetSize()];

            for (int i = 0; i < matrix1.rows.Length; i++)
            {
                for (int j = 0; j < matrix2.rows[0].GetSize(); j++)
                {
                    components[i, j] = Vector.GetScalarProduct(matrix1.rows[i], matrix2.GetColumn(j));
                }
            }

            return new Matrix(components);
        }

        public override string ToString()
        {
            return $"{{{string.Join<Vector>(", ", rows)}}}";
        }

        private static void CheckSize(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.rows.Length != matrix2.rows.Length || matrix1.rows[0].GetSize() != matrix2.rows[0].GetSize())
            {
                throw new ArgumentException($"The number of rows and columns of the matrices must be equal. Matrix1 rows: {matrix1.rows.Length}, columns: {matrix1.rows[0].GetSize()}. Matrix2 rows: {matrix2.rows.Length}, columns: {matrix2.rows[0].GetSize()}.");
            }
        }

        private static void CheckIndex(int index, int border, string name)
        {
            if (index < 0 || index >= border)
            {
                throw new ArgumentOutOfRangeException(name, $"The index goes beyond the boundary[0, {border}] of the vector. Current index value - {index}.");
            }
        }
    }
}