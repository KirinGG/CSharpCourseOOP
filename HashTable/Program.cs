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

           /* foreach (string e in hashTable)
            {
                Console.WriteLine(e);
            }

            hashTable.Remove("101");

            foreach (string e in hashTable)
            {
                Console.WriteLine(e);
            }*/

            Console.WriteLine(hashTable);
        }
    }
}
