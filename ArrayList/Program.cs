using System;

namespace ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList<string> arrayList = new ArrayList<string>();
            arrayList.Add("Иванов");
            Console.WriteLine("Capacity: {0}", arrayList.Capacity);
            arrayList.Add("Петров");
            Console.WriteLine("Capacity: {0}", arrayList.Capacity);
            arrayList.Add("Сидоров");
            Console.WriteLine("Capacity: {0}", arrayList.Capacity);

            Console.WriteLine(arrayList);

            arrayList.Remove("Петров");
            Console.WriteLine(arrayList);
        }
    }
}
