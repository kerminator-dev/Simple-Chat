namespace ChatAPI.Exceptions
{
    public class MessageNotSendException : Exception
    {
        public MessageNotSendException() { }

        public MessageNotSendException(string message) : base(message) { }
    }
}
