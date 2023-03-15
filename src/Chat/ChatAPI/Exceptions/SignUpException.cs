namespace Chat.WebAPI.Exceptions
{
    public class SignUpException : Exception
    {
        public SignUpException(string message) : base (message) { }
    }
}
