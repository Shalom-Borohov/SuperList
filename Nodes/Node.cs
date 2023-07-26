namespace SuperList.Nodes
{
    public class Node<T>
    {
        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }
        public T Value { get; set; }

        private Node() { }

        public Node(T value)
        {
            Value = value;
            Prev = null;
            Next = null;
        }
    }
}
