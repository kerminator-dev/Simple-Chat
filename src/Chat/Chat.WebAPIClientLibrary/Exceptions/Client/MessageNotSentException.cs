namespace Chat.WebAPIClientLibrary.Exceptions.Client
{
    public class MessageNotSentException : Exception
    {
        public MessageNotSentException(string message) : base(message) { }
    }
}
