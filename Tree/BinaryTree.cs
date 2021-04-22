using System;
using System.Collections.Generic;

namespace Tree
{
    class BinaryTree<T>
    {
        private TreeNode<T> root;
        private readonly IComparer<T> comparer;

        public BinaryTree() : this(null)
        {
        }

        public BinaryTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public int Count { get; private set; }

        public void Add(T data)
        {
            var newTreeNode = new TreeNode<T>(data);

            if (root == null)
            {
                root = newTreeNode;
                return;
            }

            var currentTreeNode = root;
            TreeNode<T> parent = null;
            var isLeftBranch = false;

            while (currentTreeNode != null)
            {
                parent = currentTreeNode;
                isLeftBranch = IsLeftSide(parent, newTreeNode);
                currentTreeNode = isLeftBranch ? currentTreeNode.Left : currentTreeNode.Right;
            }

            Count++;

            if (isLeftBranch)
            {
                parent.Left = newTreeNode;
            }
            else
            {
                parent.Right = newTreeNode;
            }
        }

        private TreeNode<T> FindWithParent(T data, out TreeNode<T> parent)
        {
            var currentTreeNode = root;
            parent = null;

            if (root == null)
            {
                return null;
            }

            while (currentTreeNode != null)
            {
                var comparisonResult = Compare(data, currentTreeNode.Data);

                if (comparisonResult == 0)
                {
                    return currentTreeNode;
                }

                parent = currentTreeNode;
                currentTreeNode = (comparisonResult > 0) ? currentTreeNode.Right : currentTreeNode.Left;
            }

            return null;
        }

        public bool Contains(T data)
        {
            if (root == null)
            {
                return false;
            }

            return (FindWithParent(data, out var parent) == null) ? false : true;
        }

        public bool Remove(T data)
        {
            if (root == null)
            {
                return false;
            }

            var currentTreeNode = FindWithParent(data, out var parent);

            if (currentTreeNode == null)
            {
                return false;
            }

            Count--;

            if (currentTreeNode.Left == null && currentTreeNode.Right == null)
            {
                if (parent == null)
                {
                    root = parent;
                }
                else if (IsLeftSide(parent, currentTreeNode))
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
                    root = currentTreeNode.Right;
                }
                else if (IsLeftSide(parent, currentTreeNode))
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
                    root = currentTreeNode.Left;
                }
                else if (IsLeftSide(parent, currentTreeNode))
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
                    root = GetMinimum(currentTreeNode);
                }
                else if (IsLeftSide(parent, currentTreeNode))
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

        public void WidthTraversal(Action<T> action)
        {
            if (root == null)
            {
                return;
            }

            if (action == null)
            {
                throw new InvalidOperationException("No action set for the crawling collection!");
            }

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var treeNode = queue.Dequeue();
                action(treeNode.Data);

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

        public void TraversalInDeep(Action<T> action)
        {
            if (root == null)
            {
                return;
            }

            if (action == null)
            {
                throw new InvalidOperationException("No action set for the crawling collection!");
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var treeNode = stack.Pop();
                action(treeNode.Data);

                if (treeNode.Right != null)
                {
                    stack.Push(treeNode.Right);
                }

                if (treeNode.Left != null)
                {
                    stack.Push(treeNode.Left);
                }
            }
        }

        public void TraversalInDeepRecursive(Action<T> action)
        {
            if (root == null)
            {
                return;
            }

            if (action == null)
            {
                throw new InvalidOperationException("No action set for the crawling collection!");
            }

            RecursiveTraversal(root, action);
        }

        private static void RecursiveTraversal(TreeNode<T> treeNode, Action<T> action)
        {
            action(treeNode.Data);

            if (treeNode.Left != null)
            {
                RecursiveTraversal(treeNode.Left, action);
            }

            if (treeNode.Right != null)
            {
                RecursiveTraversal(treeNode.Right, action);
            }
        }

        private bool IsLeftSide(TreeNode<T> parent, TreeNode<T> child)
        {
            if (child == null)
            {
                return true;
            }

            var comparisonResult = Compare(parent.Data, child.Data);

            return comparisonResult > 0;
        }

        private static TreeNode<T> GetMinimum(TreeNode<T> treeNode)
        {
            var parent = treeNode;
            var child = treeNode.Right;
            var isLeftSide = child.Left != null;

            while (child.Left != null)
            {
                parent = child;
                child = child.Left;
            }

            if (isLeftSide)
            {
                parent.Left = child.Right;
            }
            else
            {
                parent.Right = child.Right;
            }

            child.Left = treeNode.Left;
            child.Right = treeNode.Right;

            return child;
        }

        private int Compare(T data1, T data2)
        {
            if (comparer != null)
            {
                return comparer.Compare(data1, data2);
            }

            if (data1 == null && data2 == null)
            {
                return 0;
            }

            if (data1 == null)
            {
                return -1;
            }

            if (data2 == null)
            {
                return 1;
            }

            IComparable<T> comparable = data1 as IComparable<T>;

            if (comparable == null)
            {
                throw new InvalidOperationException("The type must be IComparable<T>, or pass the comparator to the constructor!");
            }

            return comparable.CompareTo(data2);
        }
    }
}
