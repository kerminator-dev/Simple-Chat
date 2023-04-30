using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.DTOs.Requests
{
    public class GetUserRequestDTO
    {
        [Required(ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Min username length is 3 characters!")]
        [MaxLength(16, ErrorMessage = "Max username length is 16 characters!")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,}$", ErrorMessage = "Only alphanumeric characters are available for the username!")]
        public string Username { get; set; }
    }
}
