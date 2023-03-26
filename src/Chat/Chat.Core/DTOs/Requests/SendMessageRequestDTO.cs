using System.ComponentModel.DataAnnotations;

namespace Chat.Core.DTOs.Requests
{
    public class SendMessageRequestDTO
    {
        [Required(ErrorMessage = "Message ID is required!")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Receiver is required!")]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Static key is required!")]
        public string StaticKey { get; set; }

        [Required(ErrorMessage = "Message is required!")]
        public string Message { get; set; }
    }
}
