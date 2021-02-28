using System;

namespace IT_Academ_School
{
    class Matrix
    {
        Vector[] matrix;

        public Matrix(int rowsCount, int columnsCount)
        {
            matrix = new Vector[columnsCount];

            for (int i = 0; i < columnsCount; i++)
            {
                matrix[i] = new Vector(rowsCount);
            }
        }

        public Matrix(Matrix matrix)
        {
            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);

            this.matrix = new Vector[rowsCount];

            for (int i = 0; i < columnsCount; i++)
            {
                this.matrix[i] = new Vector(matrix.GetRow(i));
            }
        }

        public Matrix(double[,] matrix)
        {
            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);

            this.matrix = new Vector[rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                double[] temp = new double[columnsCount];

                for (int j = 0; j < columnsCount; j++)
                {
                    temp[j] = matrix[i, j];
                }

                this.matrix[i] = new Vector(temp);
            }
        }

        public Matrix(Vector[] vectors)
        {
            int rowsCount = vectors.Length;
            matrix = new Vector[rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                matrix[i] = new Vector(vectors[i]);
            }
        }

        public int GetLength(int dimension)
        {
            if (dimension > 1 || dimension < 0)
            {
                throw new ArgumentException("dimension value from 0 to 1.", nameof(dimension));
            }

            if (dimension == 0)
            {
                return matrix.GetLength(0);
            }

            return matrix[0].GetSize();
        }

        public Vector GetRow(int rowsCount)
        {
            return matrix[rowsCount];
        }

        public void SetRow(int rowsCount, Vector vector)
        {
            matrix[rowsCount] = new Vector(vector);
        }

        public Vector GetColumn(int columnsCount)
        {
            double[] temp = new double[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                temp[i] = matrix[i].GetElement(columnsCount);
            }

            return new Vector(temp);
        }

        public Matrix Transpose()
        {
            Vector[] temp = new Vector[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                temp[i] = GetColumn(i);
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = temp[i];
            }

            return this;
        }

        public Matrix ScalarMultiplication(double scalar)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].GetSize(); j++)
                {
                    matrix[i].SetElement(j, matrix[i].GetElement(j) * scalar);
                }
            }

            return this;
        }

        public double GetDeterminant()
        {
            return CalculateDeterminant(this);
        }

        private double CalculateDeterminant(Matrix matrix)
        {
            if (matrix.GetLength(0) == 2)
            {
                return matrix.GetRow(0).GetElement(0) * matrix.GetRow(1).GetElement(1) - matrix.GetRow(0).GetElement(1) * matrix.GetRow(1).GetElement(0);
            }

            double result = 0;
            int i = 0;

            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                Matrix minor = GetMinor(matrix, i, j);
                result += matrix.GetRow(0).GetElement(j) * Math.Pow(-1, (i + 1) + (i + j)) * CalculateDeterminant(minor);
            }

            return result;
        }

        private Matrix GetMinor(Matrix matrix, int row, int column)
        {
            if (matrix.GetLength(0) == 2)
            {
                return matrix;
            }

            double[,] temp = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            int rowCount = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row)
                {
                    continue;
                }

                int columnCount = 0;

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == column)
                    {
                        continue;
                    }

                    temp[rowCount, columnCount] = matrix.GetRow(i).GetElement(j);
                    columnCount++;
                }

                rowCount++;
            }

            return new Matrix(temp);
        }

        public Matrix GetVectorMultiplication(Vector vector)
        {
            double[] temp = new double[vector.GetSize()];

            for (int i = 0; i < matrix.Length; i++)
            {
                double sum = 0;

                for (int j = 0; j < vector.GetSize(); j++)
                {
                    sum += matrix[i].GetElement(j) * vector.GetElement(j);
                }

                temp[i] = sum;
            }

            matrix = new Vector[] { new Vector(temp) };

            return this;
        }

        public Matrix Addition(Matrix matrix)
        {
            int rowsCount = GetLength(0);
            int columnsCount = GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    double elementsAmount = this.matrix[i].GetElement(j) + matrix.GetRow(i).GetElement(j);
                    this.matrix[i].SetElement(j, elementsAmount);
                }
            }

            return this;
        }

        public Matrix Subtraction(Matrix matrix)
        {
            int rowsCount = GetLength(0);
            int columnsCount = GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    double elementsDifference = this.matrix[i].GetElement(j) - matrix.GetRow(i).GetElement(j);
                    this.matrix[i].SetElement(j, elementsDifference);
                }
            }

            return this;
        }

        public static Matrix Addition(Matrix matrix1, Matrix matrix2)
        {
            int rowsCount = matrix1.GetLength(0);
            int columnsCount = matrix1.GetLength(1);
            double[,] temp = new double[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    double elementsAmount = matrix1.GetRow(i).GetElement(j) + matrix2.GetRow(i).GetElement(j);
                    temp[i, j] = elementsAmount;
                }
            }

            return new Matrix(temp);
        }

        public static Matrix Subtraction(Matrix matrix1, Matrix matrix2)
        {
            int rowsCount = matrix1.GetLength(0);
            int columnsCount = matrix1.GetLength(1);
            double[,] temp = new double[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    double elementsDifference = matrix1.GetRow(i).GetElement(j) - matrix2.GetRow(i).GetElement(j);
                    temp[i, j] = elementsDifference;
                }
            }

            return new Matrix(temp);
        }

        public static Matrix MatrixMultiplication(Matrix matrix1, Matrix matrix2)
        {
            int rowsCount = matrix1.GetLength(0);
            int columnsCount = matrix2.GetLength(1);
            double[,] temp = new double[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    Vector rowElements = matrix1.GetRow(i);
                    Vector columnsElements = matrix2.GetColumn(j);

                    double elementsMultiplication = 0;

                    for (int k = 0; k < rowElements.GetSize(); k++)
                    {
                        elementsMultiplication += rowElements.GetElement(k) * columnsElements.GetElement(k);
                    }

                    temp[i, j] = elementsMultiplication;
                }
            }

            return new Matrix(temp);
        }

        public override string ToString()
        {
            string[] result = new string[GetLength(0)];

            for (int i = 0; i < GetLength(0); i++)
            {
                result[i] = matrix[i].ToString();
            }

            return $"{{{string.Join(",", result)}}}";
        }
    }
}
