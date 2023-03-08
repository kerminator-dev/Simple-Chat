namespace Chat.WebAPIClientLibrary.Exceptions
{
    internal class UnexpectedHttpResponseException : Exception
    {
        public UnexpectedHttpResponseException(string message) : base(message) { }
    }
}
