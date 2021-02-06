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

        public Range GetIntersection(Range range)
        {
            if ((range == null) || (this.From > range.To) || (range.From > this.To))
            {
                return null;
            }

            double beginRange = (range.From >= this.From) ? range.From : this.From;
            double endRange = (range.To <= this.To) ? range.To : this.To;

            return new Range(beginRange, endRange);
        }

        public Range[] GetUnion(Range range)
        {
            Range[] result = new Range[2] { null, null };

            if (range == null)
            {
                return result;
            }

            if (this.GetIntersection(range) == null)
            {
                result[0] = this;
                result[1] = range;

                return result;
            }

            double beginRange = (range.From < this.From) ? range.From : this.From;
            double endRange = (range.To > this.To) ? range.To : this.To;

            result[0] = new Range(beginRange, endRange);

            return result;
        }

        public Range[] GetDifference(Range range)
        {
            Range[] result = new Range[2] { null, null };

            if ((range == null)|| (this.Equals(range)))
            {
                return result;
            }

            Range intervalsIntersection = this.GetIntersection(range);

            if (intervalsIntersection == null)
            {
                result[0] = this;
                result[1] = range;

                return result;
            }

            double beginRange = (range.From < this.From) ? range.From : this.From;
            double endRange = (range.To > this.To) ? range.To : this.To;

            result[0] = new Range(beginRange, intervalsIntersection.From);
            result[1] = new Range(intervalsIntersection.To, endRange);

            return result;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.From, this.To);
        }
    }
}