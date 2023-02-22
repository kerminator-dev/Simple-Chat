using System.ComponentModel.DataAnnotations;

namespace ChatAPI.DTOs.Requests
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Min username length is 3 characters!")]
        [MaxLength(16, ErrorMessage = "Max username length is 16 characters!")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,20}$")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(6, ErrorMessage = "Min password length is 6 characters!")]
        [MaxLength(20, ErrorMessage = "Max password length is 20 characters!")]
        [RegularExpression(@"^[a-zA-Z0-9]{4,20}$", ErrorMessage = "Password may contains digits and letters. Length of password is 4-20 characters!")]
        public string Password { get; set; }
    }
}
