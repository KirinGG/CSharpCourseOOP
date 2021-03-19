using System;
using System.IO;
using System.Collections.Generic;

/*
1. Прочитать в список все строки из файла.
2. Есть список из целых чисел. Удалить из него все четные числа. В этой задаче новый список создавать нельзя.
3. Есть список из целых чисел, в нём некоторые числа могут повторяться. Надо создать новый список, в котором будут элементы первого списка в таком же порядке,
   но без повторений Например, был список [1, 5, 2, 1, 3, 5], должен получиться новый список [1, 5, 2, 3].
*/

namespace ArrayListHome
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listFileLines = GetFileLinesList("..\\..\\..\\in.txt");

            foreach (string item in listFileLines)
            {
                Console.WriteLine(item);
            }

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 5, 5, 5, 6, 7, 8, 9 };

            RemoveEvenNumbers(numbers);
            Console.WriteLine(string.Join(", ", numbers));

            List<int> numbersWithoutRepetitions = GetNumbersWithoutRepetitions(numbers, numbers.Count);
            Console.WriteLine(string.Join(", ", numbersWithoutRepetitions));
        }

        public static List<string> GetFileLinesList(string filePath)
        {
            List<string> fileLinesList = new List<string>();

            try
            {
                using StreamReader reader = new StreamReader(filePath);
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    fileLinesList.Add(currentLine);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Не удалось загрузить данные из файла в список по причине: {ex.Message}.");
            }

            return fileLinesList;
        }

        public static void RemoveEvenNumbers(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    numbers.RemoveAt(i);
                    i--;
                }
            }
        }

        public static List<int> GetNumbersWithoutRepetitions(List<int> numbers, int capacity)
        {
            List<int> numbersWithoutRepetitions = new List<int>(capacity);

            foreach (int number in numbers)
            {
                if (!numbersWithoutRepetitions.Contains(number))
                {
                    numbersWithoutRepetitions.Add(number);
                }
            }

            return numbersWithoutRepetitions;
        }
    }
}

