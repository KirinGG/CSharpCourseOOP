using System;
using System.Collections.Generic;

namespace ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList<string> arrayList = new ArrayList<string>
            {
                "Иванов",
                "Петров",
                "Сидоров",
                null
            };

            Console.WriteLine("--- Add ---");
            Console.WriteLine(arrayList);

            Console.WriteLine("--- IndexOf ---");
            Console.WriteLine($"Тест 1-> Петров: {arrayList.IndexOf("Петров")}.");
            Console.WriteLine($"Тест 2-> Нет: {arrayList.IndexOf("Нет")}.");
            Console.WriteLine($"Тест 3-> null: {arrayList.IndexOf(null)}.");

            Console.WriteLine("--- Remove ---");
            arrayList.Remove(null);
            Console.WriteLine(arrayList);

            Console.WriteLine("--- Insert ---");
            arrayList.Insert(1, "Кашпировский");
            Console.WriteLine(arrayList);

            Console.WriteLine("--- RemoveAT ---");
            arrayList.RemoveAt(0);
            Console.WriteLine(arrayList);

            Console.WriteLine("--- trimToSize ---");
            Console.WriteLine(arrayList.Capacity);
            arrayList.TrimExpress();
            Console.WriteLine(arrayList);
            Console.WriteLine(arrayList.Capacity);

            Console.WriteLine("--- CopyTo ---");
            string[] str = new string[16];
            arrayList.CopyTo(str, 2);

            Console.WriteLine("--- Clear ---");
            arrayList.Clear();
            Console.WriteLine(arrayList);
        }
    }
}
