namespace Chat.Core.DTOs.Responses
{
    public class UserContactsResponseDTO
    {
        public IEnumerable<string> Usernames { get; set; }

        public UserContactsResponseDTO(IEnumerable<string> usernames)
        {
            Usernames = usernames;
        }
    }
}
