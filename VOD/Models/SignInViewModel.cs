using System.ComponentModel.DataAnnotations;

namespace VOD.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email must be provided.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is missing or invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be provided.")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}