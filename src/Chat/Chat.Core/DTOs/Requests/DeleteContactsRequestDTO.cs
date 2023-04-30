namespace Chat.Core.DTOs.Requests
{
    public class DeleteContactsRequestDTO
    {
        public IEnumerable<string> Usernames { get; set; }
    }
}
