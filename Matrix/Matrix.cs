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
                throw new ArgumentException($"The number of rows must be greater than zero! rowsCount: {rowsCount}.", nameof(rowsCount));
            }

            if (columnsCount < 1)
            {
                throw new ArgumentException($"The number of columns must be greater than zero! columnsCount: {columnsCount}.", nameof(columnsCount));
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
                throw new ArgumentNullException(nameof(matrix), "The matrix cannot be empty!");
            }

            rows = new Vector[matrix.rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = matrix.GetRow(i);
            }
        }

        public Matrix(double[,] components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components), "The array cannot be null!");
            }

            if (components.GetLength(0) == 0 || components.GetLength(1) == 0)
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
            if (vectors.Length == 0)
            {
                throw new ArgumentException("The vector cannot be empty!", nameof(vectors));
            }

            if (vectors == null)
            {
                throw new ArgumentNullException(nameof(vectors), "The vector cannot be null!");
            }

            var maxColumnsCount = GetMaxSize(vectors);
            rows = new Vector[vectors.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                double[] row = new double[maxColumnsCount];

                for (int j = 0; j < vectors[i].GetSize(); j++)
                {
                    row[j] = vectors[i].GetComponent(j);
                }

                rows[i] = new Vector(row);
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

        public int GetColumnsNumber()
        {
            return rows[0].GetSize();
        }

        public Vector GetRow(int rowIndex)
        {
            CheckIndex(rowIndex, rows.Length, nameof(rowIndex));

            return new Vector(rows[rowIndex]);
        }

        public void SetRow(int rowIndex, Vector vector)
        {
            CheckIndex(rowIndex, rows.Length, nameof(rowIndex));

            if (vector.GetSize() != GetColumnsNumber())
            {
                throw new ArgumentException($"invalid vector size. Vector size: {vector.GetSize()}, number of matrix columns: {GetColumnsNumber()}", nameof(vector));
            }

            rows[rowIndex] = new Vector(vector);
        }

        public Vector GetColumn(int columnIndex)
        {
            CheckIndex(columnIndex, GetColumnsNumber(), nameof(columnIndex));

            double[] column = new double[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                column[i] = rows[i].GetComponent(columnIndex);
            }

            return new Vector(column);
        }

        public Matrix Transpose()
        {
            Vector[] transposedRows = new Vector[GetColumnsNumber()];

            for (int i = 0; i < GetColumnsNumber(); i++)
            {
                transposedRows[i] = GetColumn(i);
            }

            rows = transposedRows;

            return this;
        }

        public Matrix MultiplyByScalar(double scalar)
        {
            foreach (var row in rows)
            {
                row.MultiplyByScalar(scalar);
            }

            return this;
        }

        public double GetDeterminant()
        {
            return CalculateDeterminant(this);
        }

        private static double CalculateDeterminant(Matrix matrix)
        {
            if (matrix.rows.Length != matrix.GetColumnsNumber())
            {
                throw new InvalidOperationException($"The matrix must be square! Rows: {matrix.rows.Length}, columns: {matrix.GetColumnsNumber()}.");
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
            double[,] array = new double[matrix.rows.Length - 1, matrix.GetColumnsNumber() - 1];
            int arrayRowIndex = 0;

            for (int i = 0; i < matrix.rows.Length; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }

                int arrayColumnIndex = 0;

                for (int j = 0; j < matrix.GetColumnsNumber(); j++)
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
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector), "The vector cannot be null!");
            }

            if (GetColumnsNumber() != vector.GetSize())
            {
                throw new ArgumentException($"The number of columns of the matrix must be equal to the number of elements of the vector! Matrix columns:{GetColumnsNumber()}, vector length:{vector.GetSize()}", nameof(vector));
            }

            double[] array = new double[rows.Length];

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
                rows[i].Add(matrix.rows[i]);
            }

            return this;
        }

        public Matrix Subtract(Matrix matrix)
        {
            CheckSize(matrix, this);

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].Subtract(matrix.rows[i]);
            }

            return this;
        }

        public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
        {
            CheckSize(matrix1, matrix2);

            return new Matrix(matrix1).Add(matrix2);
        }

        public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
        {
            CheckSize(matrix1, matrix2);

            return new Matrix(matrix1).Subtract(matrix2);
        }

        public static Matrix GetProduct(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null)
            {
                throw new ArgumentNullException(nameof(matrix1), "The matrixes cannot be null!");
            }

            if (matrix2 == null)
            {
                throw new ArgumentNullException(nameof(matrix2), "The matrixes cannot be null!");
            }

            if (matrix1.GetColumnsNumber() != matrix2.rows.Length)
            {
                throw new ArgumentException($"The number of columns in the 1st matrix must be equal to the number of rows in the second matrix! Matix1 columns: {matrix1.GetColumnsNumber()}, matrix2 rows: {matrix2.rows.Length}");
            }

            double[,] components = new double[matrix1.rows.Length, matrix2.GetColumnsNumber()];

            for (int i = 0; i < matrix1.rows.Length; i++)
            {
                for (int j = 0; j < matrix2.GetColumnsNumber(); j++)
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
            if (matrix1.rows.Length != matrix2.rows.Length || matrix1.GetColumnsNumber() != matrix2.GetColumnsNumber())
            {
                throw new ArgumentException($"The number of rows and columns of the matrices must be equal. Matrix1 rows: {matrix1.rows.Length}, columns: {matrix1.GetColumnsNumber()}. Matrix2 rows: {matrix2.rows.Length}, columns: {matrix2.GetColumnsNumber()}.");
            }
        }

        private static void CheckIndex(int index, int border, string name)
        {
            if (index < 0 || index >= border)
            {
                throw new ArgumentOutOfRangeException(name, $"The index goes beyond the boundary (0, {border}] of the vector. Current index value - {index}.");
            }
        }
    }
}