using System.ComponentModel.DataAnnotations;

namespace ChatAPI.DTOs.Requests
{
    public class SendMessageRequestDTO
    {
        [Required(ErrorMessage = "Receiver is required!")]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Message is required!")]
        public string Message { get; set; }
    }
}
