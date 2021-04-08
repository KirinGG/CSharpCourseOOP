namespace Tree
{
    class TreeNode<T>
    {
        public TreeNode<T> Left { get; set; }

        public TreeNode<T> Right { get; set; }

        public T Data { get; }

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

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
