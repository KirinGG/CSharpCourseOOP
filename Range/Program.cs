using System;

namespace Range
{
    class Program
    {
        static void Main(string[] args)
        {
            Range range1 = new Range(1, 5);
            Range range2 = new Range(6, 7);

            Range intersection = range1.GetIntersection(range2);
            Console.WriteLine("Пересечение интервалов {0} и {1}:", range1.ToString(), range2.ToString());
            Range.Print(intersection);

            Range[] union = range1.GetUnion(range2);
            Console.WriteLine("Объединение интервалов {0} и {1}:", range1.ToString(), range2.ToString());
            Range.Print(union);

            Range[] difference = range1.GetDifference(range2);
            Console.WriteLine("Разность интервалов {0} и {1}:", range1.ToString(), range2.ToString());
            Range.Print(difference);
        }
    }
}
