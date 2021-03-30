using System;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> binaryTree = new BinaryTree<int>();
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
            binaryTree.WidthTraversal();

            Console.WriteLine("Heght traversal:");
            binaryTree.HeightTraversal();

            Console.WriteLine("Width recurcive traversal:");
            binaryTree.HeightTraversalRecursive();

            Console.WriteLine("Remove:");
            binaryTree.Remove(9);
            binaryTree.HeightTraversal();
        }
    }
}
