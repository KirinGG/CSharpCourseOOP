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
            if (range == null)
            {
                return null;
            }

            if ((this.From > range.To) || (range.From > this.To))
            {
                return null;
            }

            double beginRange = (range.From >= this.From) ? range.From : this.From;
            double endRange = (range.To <= this.To) ? range.To : this.To;

            return new Range(beginRange, endRange);
        }

        public override string ToString()
        {
            return string.Format("Range of numbers from {0} to {1}", this.From, this.To);
        }
    }
}