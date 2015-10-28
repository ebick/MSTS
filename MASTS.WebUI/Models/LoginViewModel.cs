using System.ComponentModel.DataAnnotations;

namespace MASTS.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}