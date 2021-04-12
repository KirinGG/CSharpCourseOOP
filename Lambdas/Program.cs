using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new[]
            {
                new Person("Иван", 16),
                new Person("Антон", 17),
                new Person("Олег", 18),
                new Person("Лена", 19),
                new Person("Ира", 20),
                new Person("Иван", 21),
                new Person("Антон", 22)
            };

            PrintUniqueNames(persons);
            PrintYoungPersons(persons);
            PrintPersonsByAge(persons);
            PrintMiddleAgedPersons(persons);

            Console.WriteLine("Введите количество элементов:");
            var elementsCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Корни:");
            PrintSquareRoots(elementsCount);

            Console.WriteLine("Числа Фибоначчи:");
            PrintFibonacciNumbers(elementsCount);
        }

        public static IEnumerable<double> GetSquareRoots()
        {
            var i = 0;

            while (true)
            {
                yield return Math.Sqrt(i);
                ++i;
            }
        }

        public static IEnumerable<double> GetFibonacciNumbers()
        {
            var previousFibonacciNumber = 0;
            var currentFibonacciNumber = 1;

            yield return previousFibonacciNumber;

            while (true)
            {
                var nextFibonacciNumber = previousFibonacciNumber + currentFibonacciNumber;
                previousFibonacciNumber = currentFibonacciNumber;
                currentFibonacciNumber = nextFibonacciNumber;

                yield return previousFibonacciNumber;
            }
        }

        public static void PrintUniqueNames(Person[] persons)
        {
            var uniqueNames = persons
                .Select(p => p.Name)
                .Distinct()
                .ToList();

            Console.WriteLine($"Уникальные имена: {string.Join(", ", uniqueNames)}.");
        }

        public static void PrintYoungPersons(Person[] persons)
        {
            var youngPersons = persons
                .Where(p => p.Age < 18)
                .ToList();

            Console.WriteLine($"Список людей младше 18: {string.Join(", ", youngPersons.Select(p => p.Name))} средний возраст составляет: {youngPersons.Average(p => p.Age)}.");
        }

        public static void PrintPersonsByAge(Person[] persons)
        {
            var personsByAge = persons
                 .GroupBy(p => p.Name)
                 .ToDictionary(g => g.Key, g => g.Average(p1 => p1.Age));

            foreach (var keyValuePair in personsByAge)
            {
                Console.WriteLine(keyValuePair.Key + " - " + keyValuePair.Value);
            }
        }

        public static void PrintMiddleAgedPersons(Person[] persons)
        {
            var middleAgedPersons = persons
                .Where(p => p.Age >= 20 && p.Age <= 45)
                .OrderByDescending(p => p.Age);

            Console.WriteLine($"Люди, возраст которых от 20 до 45: {string.Join(", ", middleAgedPersons.Select(p => p.Name))}.");
        }

        public static void PrintSquareRoots(int elementsCount)
        {
            foreach (var element in GetSquareRoots().Take(elementsCount))
            {
                Console.WriteLine(element);
            }
        }

        public static void PrintFibonacciNumbers(int elementsCount)
        {
            foreach (var element in GetFibonacciNumbers().Take(elementsCount))
            {
                Console.WriteLine(element);
            }
        }
    }
}
