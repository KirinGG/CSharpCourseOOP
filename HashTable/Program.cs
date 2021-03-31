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
            hashTable.Clear();
            Console.WriteLine(hashTable);
        }
    }
}
