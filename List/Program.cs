using System;

namespace SinglyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new SinglyLinkedList<string>();

            for (var i = 0; i < 10; i++)
            {
                list.AddFirst(i.ToString());
            }

            Console.WriteLine(list);

            list.RemoveFirst();
            Console.WriteLine(list);

            list.AddFirst(null);

            list.Remove("5");
            Console.WriteLine(list);

            list.Reverse();
            Console.WriteLine(list);

            Console.WriteLine(list.Copy());

            list.Insert(3, "66");
            Console.WriteLine(list);

            list.Remove(null);
            Console.WriteLine(list);

            list.Insert(4, "33");
            Console.WriteLine(list);

            list.Insert(list.Count, "75");
            Console.WriteLine(list);

            Console.WriteLine(list.Get(list.Count - 1));
        }
    }
}
