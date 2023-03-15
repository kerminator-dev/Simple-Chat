using System.ComponentModel.DataAnnotations;

namespace Chat.Core.DTOs.Requests
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Min username length is 3 characters!")]
        [MaxLength(16, ErrorMessage = "Max username length is 16 characters!")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,}$", ErrorMessage = "Only alphanumeric characters are available for the username!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(3, ErrorMessage = "Min password length is 3 characters!")]
        [MaxLength(20, ErrorMessage = "Max password length is 20 characters!")]
        public string Password { get; set; }
    }
}
