﻿using System;

namespace Range
{
    public class Range
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
            if ((From >= range.To) || (range.From >= To))
            {
                return null;
            }

            return new Range(Math.Max(range.From, From), Math.Min(range.To, To));
        }

        public Range[] GetUnion(Range range)
        {
            if ((From > range.To) || (To < range.From))
            {
                return new Range[] { new Range(From, To), new Range(range.From, range.To) };
            }

            return new Range[] { new Range(Math.Min(range.From, From), Math.Max(range.To, To)) };
        }

        public Range[] GetDifference(Range range)
        {
            if (From >= range.From)
            {
                if (From > range.To)
                {
                    return new Range[] { new Range(From, To) };
                }

                if (To <= range.To)
                {
                    return new Range[] { };
                }

                return new Range[] { new Range(range.To, To) };
            }

            if (To < range.From)
            {
                return new Range[] { new Range(From, To) };
            }

            if (To <= range.To)
            {
                return new Range[] { new Range(From, range.From) };
            }

            return new Range[] { new Range(From, range.From), new Range(range.To, To) };
        }

        public override string ToString()
        {
            return $"({From}; {To})";
        }

        public static string GetString(params Range[] ranges)
        {
            if (ranges.Length == 0)
            {
                return "[]";
            }

            return $"[{string.Join<Range>(", ", ranges)}]";
        }
    }
}