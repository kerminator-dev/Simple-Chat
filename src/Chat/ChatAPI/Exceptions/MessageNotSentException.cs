namespace ChatAPI.Exceptions
{
    public class MessageNotSentException : Exception
    {
        public MessageNotSentException() { }

        public MessageNotSentException(string message) : base(message) { }
    }
}
