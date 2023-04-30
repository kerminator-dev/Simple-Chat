using System.ComponentModel.DataAnnotations;

namespace Chat.Core.DTOs.Requests
{
    public class SendTextMessageRequestDTO
    {
        [Required(ErrorMessage = "Message ID is required!")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Receiver is required!")]
        public string Receiver { get; set; }
        [Required(ErrorMessage = "Message is required!")]
        [MaxLength(5000, ErrorMessage = "Max message length is 5000 symbols!")]
        public string Message { get; set; }
    }
}
