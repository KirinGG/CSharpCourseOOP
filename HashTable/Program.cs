using System;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Add");
            HashTable<string> hashTable = new HashTable<string>();
            hashTable.Add("1");
            hashTable.Add("101");
            hashTable.Add("5");
            hashTable.Add("1");
            hashTable.Add(null);
            Console.WriteLine(hashTable);

            Console.WriteLine("Contains");
            Console.WriteLine(hashTable.Contains("99"));

            Console.WriteLine("Remove");
            hashTable.Remove("7");
            hashTable.Remove(null);
            Console.WriteLine(hashTable);

            Console.WriteLine("CopyTo");
            string[] array = new string[hashTable.Count + 2];
            hashTable.CopyTo(array, 2);
            Console.WriteLine(string.Join(", ", array));

            Console.WriteLine("Clear");
            hashTable.Clear();
            Console.WriteLine(hashTable);
        }
    }
}
