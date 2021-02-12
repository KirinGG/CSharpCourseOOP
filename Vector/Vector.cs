using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    class Vector
    {
        double[] elements;

        public Vector(int n)
        {
            elements = new double[n];

            for(int i = 0; i < n; i++)
            {
                elements[i] = 0;
            }
        }

        public override string ToString()
        {
            return String.Format("{{{0}}}", String.Join(",", elements));
        }
    }
}
