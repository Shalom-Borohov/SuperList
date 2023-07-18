namespace SuperList.Events.EventArgs
{
    public class ChangeEventArgs<T> : System.EventArgs
    {
        public string Message { get; set; }
        public T Value { get; set; }

        public ChangeEventArgs(string message, T value)
        {
            Message = message;
            Value = value;
        }
    }
}
