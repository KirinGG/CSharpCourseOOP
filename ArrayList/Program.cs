using System;

namespace ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayList = new ArrayList<string>
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
            Console.WriteLine(arrayList.Capacity);
            arrayList.Add("Name 1");
            arrayList.Add("Name 2");
            arrayList.Add("Name 3");
            arrayList.Add("Name 4");
            arrayList.Add("Name 5");
            arrayList.RemoveAt(arrayList.Count);
            Console.WriteLine(arrayList);

            Console.WriteLine("--- trimToSize ---");
            Console.WriteLine(arrayList.Capacity);
            arrayList.TrimExcess();
            Console.WriteLine(arrayList);
            Console.WriteLine(arrayList.Capacity);

            Console.WriteLine("--- CopyTo ---");
            var str = new string[16];
            arrayList.CopyTo(str, 2);

            Console.WriteLine("--- Clear ---");
            arrayList.Clear();
            Console.WriteLine(arrayList);
        }
    }
}
