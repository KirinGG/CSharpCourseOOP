using System;
using System.Collections.Generic;

namespace Tree
{
    class BinaryTree<T>
    {
        private TreeNode<T> root;
        private IComparer<T> comparer;

        public BinaryTree(IComparer<T> comparer = null)
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
                currentTreeNode = (isLeftBranch) ? currentTreeNode.Left : currentTreeNode.Right;
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

            IComparable<T> comparable = data as IComparable<T>;

            if (comparer == null & comparable == null)
            {
                throw new InvalidOperationException("There is no way to compare elements!");
            }

            while (currentTreeNode != null)
            {
                var comparisonResult = (comparer != null) ? comparer.Compare(data, currentTreeNode.Data) : comparable.CompareTo(currentTreeNode.Data);

                if (comparisonResult == 0)
                {
                    return currentTreeNode;
                }

                parent = currentTreeNode;
                currentTreeNode = (comparisonResult > 0) ? currentTreeNode.Right : currentTreeNode.Left;
            }

            return null;
        }

        public TreeNode<T> Find(T data)
        {
            if (root == null)
            {
                return null;
            }

            return FindWithParent(data, out TreeNode<T> parent);
        }

        public bool Remove(T data)
        {
            if (root == null)
            {
                return false;
            }

            var currentTreeNode = FindWithParent(data, out TreeNode<T> parent);

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
                    parent = currentTreeNode.Left;
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

        public void WidthTraversal(Action<TreeNode<T>> action = null)
        {
            if (root == null)
            {
                return;
            }

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var treeNode = queue.Dequeue();

                if (action != null)
                {
                    action(treeNode);
                }

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

        public void TraversalInDeep(Action<TreeNode<T>> action = null)
        {
            if (root == null)
            {
                return;
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var treeNode = stack.Pop();

                if (action != null)
                {
                    action(treeNode);
                }

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

        public void TraversalInDeepRecursive(Action<TreeNode<T>> action = null)
        {
            if (root == null)
            {
                return;
            }

            RecursiveTraversal(root, action);
        }

        private void RecursiveTraversal(TreeNode<T> treeNode, Action<TreeNode<T>> action = null)
        {
            if (action != null)
            {
                action(treeNode);
            }

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

            IComparable<T> comparable = parent.Data as IComparable<T>;

            if (comparer == null & comparable == null)
            {
                throw new InvalidOperationException("There is no way to compare elements!");
            }

            var comparisonResult = (comparer != null) ? comparer.Compare(parent.Data, child.Data) : comparable.CompareTo(child.Data);

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
    }
}
