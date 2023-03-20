namespace Chat.WebAPIClientLibrary.Exceptions.Client
{
    internal class TokenNotRefreshedException : Exception
    {
        public TokenNotRefreshedException(string message) : base(message) { }
    }
}
