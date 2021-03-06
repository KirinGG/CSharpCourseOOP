﻿using System;

namespace Range
{
    class Program
    {
        static void Main(string[] args)
        {
            Range range1 = new Range(1, 7);
            Range range2 = new Range(3, 5);

            Range intersection = range1.GetIntersection(range2);
            Console.WriteLine($"Пересечение интервалов {range1} и {range2}: {Environment.NewLine}{Range.GetString(intersection)}");

            Range[] union = range1.GetUnion(range2);
            Console.WriteLine($"Объединение интервалов {range1} и {range2}: {Environment.NewLine}{Range.GetString(union)}");

            Range[] difference = range1.GetDifference(range2);
            Console.WriteLine($"Разность интервалов {range1} и {range2}: {Environment.NewLine}{Range.GetString(difference)}");
        }
    }
}
