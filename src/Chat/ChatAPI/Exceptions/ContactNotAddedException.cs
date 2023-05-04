namespace Chat.WebAPI.Exceptions
{
    public class ContactNotAddedException : Exception
    {
        public ContactNotAddedException(string message) : base(message) { }
    }
}
