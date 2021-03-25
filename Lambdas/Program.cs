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
            PrintNumbersRoot(elementsCount);
            PrintFibonacciNumbers(elementsCount);
        }

        public static IEnumerable<double> GetRoot()
        {
            int i = 0;
            while (true)
            {
                yield return Math.Sqrt(i);
                ++i;
            }
        }

        public static IEnumerable<double> GetFibonacciNumber()
        {
            int fibonacciNumberIndex = 0;
            int previousFibonacciNumber = 0;
            int currentFibonacciNumber = 1;

            while (true)
            {
                if (fibonacciNumberIndex == 0)
                {
                    fibonacciNumberIndex++;

                    yield return previousFibonacciNumber;
                }
                else if (fibonacciNumberIndex == 1)
                {
                    fibonacciNumberIndex++;

                    yield return currentFibonacciNumber;
                }

                int nextFibonacciNumber = previousFibonacciNumber + currentFibonacciNumber;
                previousFibonacciNumber = currentFibonacciNumber;
                currentFibonacciNumber = nextFibonacciNumber;
                fibonacciNumberIndex++;

                yield return previousFibonacciNumber;
            }
        }

        public static void PrintUniqueNames(Person[] persons)
        {
            var uniqueNames = persons
                .Select(s => s.Name)
                .Distinct();

            Console.WriteLine($"Уникальные имена: {string.Join(", ", uniqueNames)}.");
        }

        public static void PrintYoungPersons(Person[] persons)
        {
            var youngPersons = persons
                .Where(x => x.Age < 18);

            Console.WriteLine($"Список людей младше 18: {string.Join(", ", youngPersons.Select(s => s.Name))} средний возраст составляет:{youngPersons.Select(s => s.Age).Average()}.");
        }

        public static void PrintPersonsByAge(Person[] persons)
        {
            Dictionary<string, double> personsByAge = persons
                 .GroupBy(p => p.Name)
                 .ToDictionary(p => p.Key, p => p.ToList().Select(x => x.Age).Average());

            foreach (KeyValuePair<string, double> keyValue in personsByAge)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
        }

        public static void PrintMiddleAgedPersons(Person[] persons)
        {
            var middleAgedPersons = persons
                .Where(s => s.Age >= 20 && s.Age <= 45)
                .OrderByDescending(s => s.Age);

            Console.WriteLine($"Люди, возраст которых от 20 до 45: {string.Join(", ", middleAgedPersons.Select(s => s.Name))}.");
        }

        public static void PrintNumbersRoot(int elementsCount)
        {
            int i = 0;

            foreach (double element in GetRoot())
            {
                if (i == elementsCount)
                {
                    break;
                }

                Console.WriteLine(element);
                i++;
            }
        }

        public static void PrintFibonacciNumbers(int elementsCount)
        {
            int i = 0;

            foreach (double element in GetFibonacciNumber())
            {
                if (i == elementsCount)
                {
                    break;
                }

                Console.WriteLine(element);
                i++;
            }
        }
    }
}
