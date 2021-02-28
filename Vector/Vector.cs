using System;

namespace IT_Academ_School
{
    public class Vector
    {
        private double[] elements;

        public Vector(int dimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentException("The dimension of the vector must be > 0.", nameof(dimension));
            }

            elements = new double[dimension];

            for (int i = 0; i < dimension; i++)
            {
                elements[i] = 0;
            }
        }

        public Vector(Vector elements)
        {
            this.elements = new double[elements.GetSize()];

            for (int i = 0; i < elements.GetSize(); i++)
            {
                this.elements[i] = GetElement(i);
            }
        }

        public Vector(double[] elements)
        {
            this.elements = new double[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                this.elements[i] = elements[i];
            }
        }

        public Vector(int dimension, double[] elements)
        {
            if (dimension <= 0)
            {
                throw new ArgumentException("The dimension of the vector must be > 0.", nameof(dimension));
            }

            this.elements = new double[dimension];

            for (int i = 0; i < dimension; i++)
            {
                if (i < elements.Length)
                {
                    this.elements[i] = elements[i];
                }
                else
                {
                    this.elements[i] = 0;
                }
            }
        }

        public int GetSize()
        {
            return elements.Length;
        }

        public double GetElement(int index)
        {
            if (index > GetSize())
            {
                throw new ArgumentOutOfRangeException("The index goes beyond the boundary of the vector", nameof(index));
            }

            return elements[index];
        }

        public void SetElement(int index, double value)
        {
            if (index > GetSize())
            {
                throw new ArgumentOutOfRangeException("The index goes beyond the boundary of the vector", nameof(index));
            }

            elements[index] = value;
        }

        public Vector Addition(Vector vector)
        {
            int minLength = Math.Min(vector.GetSize(), GetSize());
            int maxLength = Math.Max(vector.GetSize(), GetSize());

            if (GetSize() == maxLength)
            {
                for (int i = 0; i < minLength; i++)
                {
                    elements[i] += vector.GetElement(i);
                }
            }
            else
            {
                double[] temp = new double[maxLength];

                for (int i = 0; i < maxLength; i++)
                {
                    temp[i] = vector.GetElement(i);

                    if (i < minLength)
                    {
                        temp[i] += elements[i];
                    }
                }

                elements = temp;
            }

            return this;
        }

        public Vector Subtraction(Vector vector)
        {
            int minLength = Math.Min(vector.GetSize(), GetSize());
            int maxLength = Math.Max(vector.GetSize(), GetSize());

            if (GetSize() == maxLength)
            {
                for (int i = 0; i < minLength; i++)
                {
                    elements[i] -= vector.GetElement(i);
                }
            }
            else
            {
                double[] temp = new double[maxLength];

                for (int i = 0; i < maxLength; i++)
                {
                    temp[i] = vector.GetElement(i);

                    if (i < minLength)
                    {
                        temp[i] = elements[i] - temp[i];
                    }
                    else
                    {
                        temp[i] *= -1;
                    }
                }

                elements = temp;
            }

            return this;
        }

        public Vector ScalarMultiplication(double scalar)
        {
            for (int i = 0; i < GetSize(); i++)
            {
                elements[i] *= scalar;
            }

            return this;
        }

        public Vector Reversal()
        {
            for (int i = 0; i < GetSize(); i++)
            {
                elements[i] *= -1;
            }

            return this;
        }

        public static Vector Addition(Vector vector1, Vector vector2)
        {
            Vector result;

            if (vector1.GetSize() > vector2.GetSize())
            {
                result = new Vector(vector1);
                result.Addition(vector2);
            }
            else
            {
                result = new Vector(vector2);
                result.Addition(vector1);
            }

            return result;
        }

        public static Vector Reversal(Vector vector1, Vector vector2)
        {
            Vector result;

            if (vector1.GetSize() > vector2.GetSize())
            {
                result = new Vector(vector1);
                result.Subtraction(vector2);
            }
            else
            {
                result = new Vector(vector2);
                result.Subtraction(vector1);
            }

            return result;
        }

        public static double ScalarMultiplication(double scalar, Vector vector1, Vector vector2)
        {
            int maxLength = Math.Max(vector1.GetSize(), vector2.GetSize());
            double scalarMultiplication = 0;

            for (int i = 0; i < maxLength; i++)
            {
                scalarMultiplication += vector1.GetElement(i) * vector2.GetElement(i);
            }

            return scalarMultiplication;
        }

        public override string ToString()
        {
            return $"{{{String.Join(",", elements)}}}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            Vector vector = (Vector)obj;

            if (vector.GetSize() != GetSize())
            {
                return false;
            }

            for (int i = 0; i < vector.GetSize(); i++)
            {
                if (vector.GetElement(i) != elements[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int prime = 37;
            int hash = GetSize();

            return prime * hash + GetElementsSum().GetHashCode();
        }

        private double GetElementsSum()
        {
            double result = 0;

            for (int i = 0; i < GetSize(); i++)
            {
                result += GetElement(i) * (i + 1);
            }

            return result;
        }
    }
}
