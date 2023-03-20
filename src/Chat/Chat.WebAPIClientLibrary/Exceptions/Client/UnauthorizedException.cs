namespace Chat.WebAPIClientLibrary.Exceptions.Client
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { } 
    }
}
