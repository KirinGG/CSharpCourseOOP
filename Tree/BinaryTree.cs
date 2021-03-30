using System;
using System.Collections.Generic;

namespace Tree
{
    class BinaryTree<T> where T : IComparable<T>
    {
        public TreeNode<T> head;

        public int Count { get; set; }

        public void Add(T data)
        {
            CheckArgument(data);

            if (head == null)
            {
                head = new TreeNode<T>(data);
                return;
            }

            TreeNode<T> currentTreeNode = head;
            TreeNode<T> parent = null;
            bool isLeftBranch = false;

            while (currentTreeNode != null)
            {
                parent = currentTreeNode;
                isLeftBranch = IsLeftSide(parent.Data, data);

                if (isLeftBranch)
                {
                    currentTreeNode = currentTreeNode.Left;
                }
                else
                {
                    currentTreeNode = currentTreeNode.Right;
                }
            }

            Count++;

            if (isLeftBranch)
            {
                parent.Left = new TreeNode<T>(data);
            }
            else
            {
                parent.Right = new TreeNode<T>(data);
            }
        }

        private TreeNode<T> FindWithParent(T data, out TreeNode<T> parent)
        {
            CheckArgument(data);

            TreeNode<T> currentTreeNode = head;
            parent = null;

            if (head == null)
            {
                return null;
            }

            while (currentTreeNode != null)
            {
                if (currentTreeNode.Data.CompareTo(data) < 0)
                {
                    parent = currentTreeNode;
                    currentTreeNode = currentTreeNode.Right;
                }
                else if (currentTreeNode.Data.CompareTo(data) > 0)
                {
                    parent = currentTreeNode;
                    currentTreeNode = currentTreeNode.Left;
                }
                else
                {
                    return currentTreeNode;
                }
            }

            return null;
        }

        public TreeNode<T> Find(T data)
        {
            CheckArgument(data);

            if (head == null)
            {
                return null;
            }

            TreeNode<T> currentTreeNode = head;

            while (currentTreeNode != null)
            {
                if (currentTreeNode.Data.CompareTo(data) < 0)
                {
                    currentTreeNode = currentTreeNode.Right;
                }
                else if (currentTreeNode.Data.CompareTo(data) > 0)
                {
                    currentTreeNode = currentTreeNode.Left;
                }
                else
                {
                    return currentTreeNode;
                }
            }

            return null;
        }

        public bool Remove(T data)
        {
            CheckArgument(data);

            if (head == null)
            {
                return false;
            }

            TreeNode<T> parent = null;
            var currentTreeNode = FindWithParent(data, out parent);

            if (currentTreeNode == null)
            {
                return false;
            }

            Count--;

            if (currentTreeNode.Left == null && currentTreeNode.Right == null)
            {
                if (parent == null)
                {
                    head = parent;
                }
                else if (IsLeftSide(parent.Data, currentTreeNode.Data))
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }
            }
            else if (currentTreeNode.Left == null)
            {
                if (parent == null)
                {
                    head = currentTreeNode.Right;
                }
                else if (IsLeftSide(parent.Data, currentTreeNode.Data))
                {
                    parent.Left = currentTreeNode.Right;
                }
                else
                {
                    parent.Right = currentTreeNode.Right;
                }
            }
            else if (currentTreeNode.Right == null)
            {
                if (parent == null)
                {
                    parent = currentTreeNode.Left;
                }
                else if (IsLeftSide(parent.Data, currentTreeNode.Data))
                {
                    parent.Left = currentTreeNode.Left;
                }
                else
                {
                    parent.Right = currentTreeNode.Left;
                }
            }
            else
            {
                if (parent == null)
                {
                    head = GetMinimum(currentTreeNode);
                }
                else if (IsLeftSide(parent.Data, currentTreeNode.Data))
                {
                    parent.Left = GetMinimum(currentTreeNode);
                }
                else
                {
                    parent.Right = GetMinimum(currentTreeNode);
                }
            }

            return true;
        }

        public void WidthTraversal()
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(head);

            while (queue.Count > 0)
            {
                var treeNode = queue.Dequeue();
                Console.WriteLine("{0}, Left:{1}, Right:{2}.", treeNode.Data, treeNode.Left, treeNode.Right);

                if (treeNode.Left != null)
                {
                    queue.Enqueue(treeNode.Left);
                }

                if (treeNode.Right != null)
                {
                    queue.Enqueue(treeNode.Right);
                }
            }
        }

        public void HeightTraversal()
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(head);

            while (stack.Count > 0)
            {
                var treeNode = stack.Pop();
                Console.WriteLine("{0}, Left:{1}, Right:{2}.", treeNode.Data, treeNode.Left, treeNode.Right);

                if (treeNode.Left != null)
                {
                    stack.Push(treeNode.Left);
                }

                if (treeNode.Right != null)
                {
                    stack.Push(treeNode.Right);
                }
            }
        }

        public void HeightTraversalRecursive()
        {
            if (head == null)
            {
                return;
            }

            RecursiveTraversal(head);
        }

        private void RecursiveTraversal(TreeNode<T> treeNode)
        {
            Console.WriteLine(treeNode.Data);

            if (treeNode.Left != null)
            {
                RecursiveTraversal(treeNode.Left);
            }

            if (treeNode.Right != null)
            {
                RecursiveTraversal(treeNode.Right);
            }
        }

        private void CheckArgument(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"The argument cannot be null!");
            }
        }

        private bool IsLeftSide(T parent, T child)
        {
            if (parent.CompareTo(child) <= 0)
            {
                return false;
            }

            return true;
        }

        private TreeNode<T> GetMinimum(TreeNode<T> treeNode)
        {
            var parent = treeNode;
            var child = treeNode.Right;

            while (child.Left != null)
            {
                parent = child;
                child = child.Left;
            }

            if (child.Right == null)
            {
                parent.Left = null;
            }
            else
            {
                parent.Left = child.Right;
            }

            child.Left = treeNode.Left;
            child.Right = treeNode.Right;

            return child;
        }
    }
}
