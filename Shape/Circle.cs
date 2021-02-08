﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double getArea()
        {
            return Math.PI * Radius * Radius;
        }

        public double getHeight()
        {
            return Radius * 2;
        }

        public double getPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public double getWidth()
        {
            return Radius * 2;
        }
    }
}
