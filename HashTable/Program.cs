using System;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable<string> hashTable = new HashTable<string>();
            hashTable.Add("1");
            hashTable.Add("101");
            hashTable.Add("5");
            hashTable.Add("1");
            Console.WriteLine(hashTable);

            hashTable.Remove("7");
            Console.WriteLine(hashTable);

            string[] array = new string[hashTable.Count + 2];
            hashTable.CopyTo(array, 2);
            Console.WriteLine(string.Join(", ", array));

            hashTable.Clear();
            Console.WriteLine(hashTable);
        }
    }
}
