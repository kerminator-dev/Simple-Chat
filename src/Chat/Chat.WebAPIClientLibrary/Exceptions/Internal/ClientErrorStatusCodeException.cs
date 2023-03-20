namespace Chat.WebAPIClientLibrary.Exceptions.Internal
{
    internal class ClientErrorStatusCodeException : Exception
    {
        public ClientErrorStatusCodeException(string message) : base(message) { }
    }
}
