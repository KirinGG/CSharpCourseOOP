using System;
using System.Linq;

namespace Range
{
    class Program
    {
        static void Main(string[] args)
        {
            Range range1 = new Range(1, 5);
            Range range2 = new Range(2, 6);
            Range range3 = new Range(6, 8);
            Range range4 = new Range(2, 4);

            Range range5 = range1.GeIntervalsIntersection(range2);
            Range range6 = range1.GeIntervalsIntersection(range3);
            Range range7 = range1.GeIntervalsIntersection(range4);

            if (range5 != null)
            {
                Console.WriteLine(range5.ToString());
            }
            if (range6 != null)
            {
                Console.WriteLine(range6.ToString());
            }
            if (range7 != null)
            {
                Console.WriteLine(range7.ToString());
            }
        }
    }
}
