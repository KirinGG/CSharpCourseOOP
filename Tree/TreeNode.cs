using System;
using System.Diagnostics.CodeAnalysis;

namespace Tree
{
    class TreeNode<T>
    {
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public T Data { get; private set; }

        public TreeNode(T data)
        {
            Data = data;
        }

        public TreeNode(T data, TreeNode<T> left, TreeNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            TreeNode<T> treeNode = (TreeNode<T>)obj;

            return Data.Equals(treeNode.Data);
         }

        public override int GetHashCode()
        {
            int prime = 37;
            int hash = 1;

            hash = prime * hash + Data.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
