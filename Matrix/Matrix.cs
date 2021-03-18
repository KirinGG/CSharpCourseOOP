using System;
using System.Text;
using Vectors;

namespace Matrix
{
    class Matrix
    {
        private Vector[] rows;
        private int rowsCount;
        private int columnsCount;

        public int RowsCount
        {
            get
            {
                return rowsCount;
            }

            private set
            {
                rowsCount = (value < 1) ? 1 : value;
            }
        }

        public int ColumnsCount
        {
            get
            {
                return columnsCount;
            }

            private set
            {
                columnsCount = (value < 1) ? 1 : value;
            }
        }

        public Matrix(int rowsCount, int columnsCount)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;

            rows = new Vector[ColumnsCount];

            for (int i = 0; i < ColumnsCount; i++)
            {
                rows[i] = new Vector(RowsCount);
            }
        }

        public Matrix(Matrix matrix)
        {
            RowsCount = matrix.RowsCount;
            ColumnsCount = matrix.ColumnsCount;

            rows = new Vector[rowsCount];

            for (int i = 0; i < columnsCount; i++)
            {
                rows[i] = matrix.GetRow(i);
            }
        }

        public Matrix(double[,] components)
        {
            if (components.GetLength(0) == 0 || components.GetLength(1) == 0)
            {
                rows = new Vector[1];
                rows[0] = new Vector(new double[1] { 0 });
            }
            else
            {
                RowsCount = components.GetLength(0);
                ColumnsCount = components.GetLength(1);

                rows = new Vector[RowsCount];

                for (int i = 0; i < RowsCount; i++)
                {
                    double[] row = new double[ColumnsCount];

                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        row[j] = components[i, j];
                    }

                    rows[i] = new Vector(row);
                }
            }
        }

        public Matrix(Vector[] vectors)
        {
            if (vectors.Length == 0)
            {
                rows = new Vector[1];
                rows[0] = new Vector(new double[1] { 0 });
            }
            else
            {
                RowsCount = vectors.Length;
                ColumnsCount = GetMaxSize(vectors);
                rows = new Vector[RowsCount];

                for (int i = 0; i < RowsCount; i++)
                {
                    double[] row = new double[rows[i].GetSize()];

                    for (int j = 0; j < rows[i].GetSize(); j++)
                    {
                        row[j] = rows[i].GetComponent(j);
                    }

                    rows[i] = new Vector(ColumnsCount, row);
                }
            }
        }

        private int GetMaxSize(Vector[] vectors)
        {

            int maxSize = vectors[0].GetSize();

            for (int i = 0; i < vectors.Length; i++)
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
            if (rowIndex < 0 || rowIndex > RowsCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"The index goes beyond the boundary[0,{RowsCount}] of the vector. Current index value - {rowIndex}.");
            }

            return new Vector(rows[rowIndex]);
        }

        public void SetRow(int rowIndex, Vector vector)
        {
            if (rowIndex < 0 || rowIndex > RowsCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"The index goes beyond the boundary[0,{RowsCount}] of the vector. Current index value - {rowIndex}.");
            }

            if (vector.GetSize() != ColumnsCount)
            {
                throw new ArgumentException($"invalid vector size. Size: {vector.GetSize()}");
            }

            rows[rowIndex] = new Vector(vector);
        }

        public Vector GetColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex > ColumnsCount)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), $"The index goes beyond the boundary[0,{ColumnsCount}] of the vector. Current index value - {columnIndex}.");
            }

            double[] column = new double[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                column[i] = rows[i].GetComponent(columnIndex);
            }

            return new Vector(column);
        }

        public Matrix Transpose()
        {
            Vector[] transposeRows = new Vector[ColumnsCount];

            for (int i = 0; i < ColumnsCount; i++)
            {
                transposeRows[i] = GetColumn(i);
            }

            rows = transposeRows;

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
            if (matrix.RowsCount != matrix.ColumnsCount)
            {
                throw new ArgumentException("The matrix must be square");
            }

            if (matrix.RowsCount == 1)
            {
                return matrix.GetRow(0).GetComponent(0);
            }

            if (matrix.RowsCount == 2)
            {
                return matrix.GetRow(0).GetComponent(0) * matrix.GetRow(1).GetComponent(1) - matrix.GetRow(0).GetComponent(1) * matrix.GetRow(1).GetComponent(0);
            }

            double result = 0;
            int i = 0;

            for (int j = 0; j < matrix.RowsCount; j++)
            {
                Matrix minor = GetMinor(matrix, i, j);
                result += matrix.GetRow(0).GetComponent(j) * Math.Pow(-1, (i + 1) + (i + j)) * CalculateDeterminant(minor);
            }

            return result;
        }

        private static Matrix GetMinor(Matrix matrix, int rowIndex, int columnIndex)
        {
            if (matrix.RowsCount == 2)
            {
                return matrix;
            }

            double[,] temp = new double[matrix.RowsCount - 1, matrix.ColumnsCount - 1];
            int rowCount = 0;

            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }

                int columnCount = 0;

                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (j == columnIndex)
                    {
                        continue;
                    }

                    temp[rowCount, columnCount] = matrix.GetRow(i).GetComponent(j);
                    columnCount++;
                }

                rowCount++;
            }

            return new Matrix(temp);
        }

        public Vector MultiplyByVector(Vector vector)
        {
            if (ColumnsCount != vector.GetSize())
            {
                throw new ArgumentException("The number of columns of the matrix must be equal to the number of elements of the vector!");
            }

            double[] vectorsScalarProduct = new double[vector.GetSize()];

            for (int i = 0; i < rows.Length; i++)
            {
                vectorsScalarProduct[i] = Vector.GetScalarProduct(rows[i], vector);
            }

            return new Vector(vectorsScalarProduct);
        }

        public Matrix Add(Matrix matrix)
        {
            if (RowsCount != matrix.RowsCount)
            {
                throw new ArgumentException("the number of rows must be equal.");
            }

            for (int i = 0; i < RowsCount; i++)
            {
                rows[i] = Vector.GetSum(rows[i], matrix.GetRow(i));
            }

            return this;
        }

        public Matrix Subtract(Matrix matrix)
        {
            if(RowsCount != matrix.RowsCount)
            {
                throw new ArgumentException("the number of rows must be equal.");
            }

            for (int i = 0; i < RowsCount; i++)
            {
                rows[i] = Vector.GetDifference(rows[i], matrix.GetRow(i));
            }

            return this;
        }

        public static Matrix GetAddition(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.RowsCount != matrix2.RowsCount)
            {
                throw new ArgumentException("the number of rows must be equal.");
            }

            Vector[] rows = new Vector[matrix1.ColumnsCount];

            for (int i = 0; i < matrix1.RowsCount; i++)
            {
                rows[i] = Vector.GetSum(matrix1.GetRow(i), matrix2.GetRow(i));
            }

            return new Matrix(rows);
        }

        public static Matrix GetSubtraction(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.RowsCount != matrix2.RowsCount)
            {
                throw new ArgumentException("the number of rows must be equal.");
            }

            Vector[] rows = new Vector[matrix1.ColumnsCount];

            for (int i = 0; i < matrix1.RowsCount; i++)
            {
                rows[i] = Vector.GetDifference(matrix1.GetRow(i), matrix2.GetRow(i));
            }

            return new Matrix(rows);
        }

        public static Matrix GetMatrixsMultiplication(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.RowsCount != matrix2.ColumnsCount)
            {
                throw new ArgumentException("The number of rows must be equal to the number of columns!");
            }

            double[,] components = new double[matrix1.RowsCount, matrix2.ColumnsCount];

            for (int i = 0; i < matrix1.RowsCount; i++)
            {
                for (int j = 0; j < matrix2.ColumnsCount; j++)
                {
                    components[i, j] = Vector.GetScalarProduct(matrix1.GetRow(i), matrix2.GetColumn(j));
                }
            }

            return new Matrix(components);
        }

        public override string ToString()
        {
            return $"{{{string.Join<Vector>(", ", rows)}}}";
        }
    }
}
