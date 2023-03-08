namespace Chat.WebAPIClientLibrary.Exceptions
{
    internal class NoHttpResponseHandlerException : Exception
    {
        public NoHttpResponseHandlerException(string message) : base(message) { }
    }
}
