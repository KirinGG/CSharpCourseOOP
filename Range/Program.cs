using System;

namespace Range
{
    class Program
    {
        static void Main(string[] args)
        {
            Range range1 = new Range(1, 5);
            Range range2 = new Range(2, 6);

            Range intersectionRanges = range1.GetIntersection(range2);

            if (intersectionRanges != null)
            {
                Console.WriteLine("Пересечение интервалов {0} и {1}: {2}", range1.ToString(), range2.ToString(), intersectionRanges.ToString());
            }
            else
            {
                Console.WriteLine("Пересечение интервалов {0} и {1}: Не пересекаются", range1.ToString(), range2.ToString());
            }

            Range[] unionRanges = range1.GetUnion(range2);
            Console.WriteLine("Объединение интервалов {0} и {1}:", range1.ToString(), range2.ToString());

            foreach (Range range in unionRanges)
            {
                if (range != null)
                {
                    Console.WriteLine(range.ToString());
                }
            }

            Range[] differenceRanges = range1.GetDifference(range2);
            Console.WriteLine("Разность интервалов {0} и {1}:", range1.ToString(), range2.ToString());

            foreach (Range range in differenceRanges)
            {
                if (range != null)
                {
                    Console.WriteLine(range.ToString());
                }
            }
        }
    }
}
