namespace Chat.WebAPIClientLibrary.Exceptions.Internal
{
    internal class UnauthorizedStatusCodeException : Exception
    {

        public UnauthorizedStatusCodeException(string message) : base(message) { }
    }
}
