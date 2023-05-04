namespace Chat.Core.DTOs.Responses
{
    public class UserContactsResponseDTO
    {
        public IEnumerable<UserResponseDTO> Contacts { get; private set; }

        public UserContactsResponseDTO(IEnumerable<UserResponseDTO> contacts)
        {
            Contacts = contacts;
        }

        public UserContactsResponseDTO()
        {
            Contacts = new List<UserResponseDTO>();
        }
    }
}
