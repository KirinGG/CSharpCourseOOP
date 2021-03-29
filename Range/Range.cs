using System;
using System.Collections.Generic;
using System.Text;

namespace Range
{
    class Range
    {
        public double From { get; set; }
        public double To { get; set; }

        public Range(double from, double to)
        {
            From = from;
            To = to;
        }

        public double GetLength()
        {
            return To - From;
        }

        public bool IsInside(double number)
        {
            double epsilon = 1.0e-10;

            return (number - From >= -epsilon) && (To - number >= -epsilon);
        }

        public Range GeIntervalsIntersection(Range range)
        {
            if(range == null)
            {
                return null;
            }

            if(range.From < this.From)
            {
                if(range.To < this.From)
                {
                    // Пересечений нет
                }

                if(range.To > this.From)
                {

                }
                return null;
            }

            return null;
        }
    }
}