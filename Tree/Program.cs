using System;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<int> print = Console.WriteLine;

            Console.WriteLine("Test 1:");
            var comparer = new IntComparer();
            var binaryTree = new BinaryTree<int>(comparer);
            binaryTree.Add(9);
            binaryTree.Add(6);
            binaryTree.Add(12);
            binaryTree.Add(3);
            binaryTree.Add(7);
            binaryTree.Add(11);
            binaryTree.Add(15);
            binaryTree.Add(1);
            binaryTree.Add(4);
            binaryTree.Add(10);
            binaryTree.Add(14);
            binaryTree.Add(16);

            Console.WriteLine("Width traversal:");
            binaryTree.WidthTraversal(print);

            Console.WriteLine("Heght traversal:");
            binaryTree.TraversalInDeep(print);

            Console.WriteLine("Width recurcive traversal:");
            binaryTree.TraversalInDeepRecursive(print);

            Console.WriteLine("Remove:");
            binaryTree.Remove(9);
            binaryTree.TraversalInDeep(print);

            Console.WriteLine("Test 2:");
            var binaryTree1 = new BinaryTree<int>();
            binaryTree1.Add(1);
            binaryTree1.Add(3);
            binaryTree1.Add(2);
            binaryTree1.Add(5);
            binaryTree1.Remove(3);
            binaryTree1.WidthTraversal(print);
        }
    }
}
