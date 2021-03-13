using System;
using System.Text;

namespace Vectors
{
    public class Vector
    {
        private double[] components;

        public Vector(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException($"The size of the vector must be > 0. Current value: {size}.", nameof(size));
            }

            components = new double[size];
        }

        public Vector(Vector vector)
        {
            components = new double[vector.components.Length];
            Array.Copy(vector.components, components, vector.components.Length);
        }

        public Vector(double[] components)
        {
            if (components.Length == 0)
            {
                throw new ArgumentException($"The size of the array must be > 0. Current value: {components.Length}.", nameof(components));
            }

            this.components = new double[components.Length];
            Array.Copy(components, this.components, components.Length);
        }

        public Vector(int size, double[] components)
        {
            if (size <= 0)
            {
                throw new ArgumentException($"The size of the vector must be > 0. Current value: {size}.", nameof(size));
            }

            this.components = new double[size];

            if (size > components.Length)
            {
                Array.Copy(components, this.components, components.Length);
            }
            else
            {
                Array.Copy(components, this.components, size);
            }
        }

        public int GetSize()
        {
            return components.Length;
        }

        public double GetComponent(int index)
        {
            if (index < 0 || index >= components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {components.Length - 1}] of the vector. Current index value: {index}.");
            }

            return components[index];
        }

        public void SetComponent(int index, double value)
        {
            if (index < 0 || index >= components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {components.Length - 1}] of the vector. Current index value: {index}.");
            }

            components[index] = value;
        }

        public Vector Add(Vector vector)
        {
            Resize(vector);

            for (int i = 0; i < vector.components.Length; i++)
            {
                components[i] += vector.components[i];
            }

            return this;
        }

        public Vector Subtract(Vector vector)
        {
            Resize(vector);

            for (int i = 0; i < vector.components.Length; i++)
            {
                components[i] -= vector.components[i];
            }

            return this;
        }

        private void Resize(Vector vector)
        {
            if (components.Length < vector.components.Length)
            {
                Array.Resize(ref components, vector.components.Length);
            }
        }

        public Vector MultiplyByScalar(double scalar)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i] *= scalar;
            }

            return this;
        }

        public Vector Reverse()
        {
            MultiplyByScalar(-1);

            return this;
        }

        public static Vector GetSum(Vector vector1, Vector vector2)
        {
            return new Vector(vector1).Add(vector2);
        }

        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            return new Vector(vector1).Subtract(vector2);
        }

        public static double GetScalarProduct(Vector vector1, Vector vector2)
        {
            if (vector1.GetSize() != vector2.GetSize())
            {
                throw new ArgumentException($"The size of the vectors must match. vector1: {vector1.GetSize()}, vector2: {vector2.GetSize()}.");
            }

            int maxSize = Math.Max(vector1.components.Length, vector2.components.Length);
            double scalarProduct = 0;

            for (int i = 0; i < maxSize; i++)
            {
                scalarProduct += vector1.components[i] * vector2.components[i];
            }

            return scalarProduct;
        }

        public override string ToString()
        {
            return new StringBuilder("{").AppendJoin(", ", components).Append("}").ToString();
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

            if (vector.components.Length != components.Length)
            {
                return false;
            }

            for (int i = 0; i < vector.components.Length; i++)
            {
                if (vector.components[i] != components[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int prime = 37;
            int hash = 1;

            foreach (double component in components)
            {
                hash = prime * hash + component.GetHashCode();
            }

            return hash;
        }
    }
}