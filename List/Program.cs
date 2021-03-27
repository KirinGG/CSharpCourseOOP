﻿using System;

namespace SinglyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            SinglyLinkedList<string> list = new SinglyLinkedList<string>();

            for (int i = 0; i < 10; i++)
            {
                list.AddFirst(i.ToString());
            }
            
            Console.WriteLine(list);

            list.RemoveFirst();
            Console.WriteLine(list);

            list.Remove("5");
            Console.WriteLine(list);

            list.Reverse();
            Console.WriteLine(list);

            Console.WriteLine(list.Copy());
        }
    }
}
