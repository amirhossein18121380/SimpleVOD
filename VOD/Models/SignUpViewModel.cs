using System.ComponentModel.DataAnnotations;

namespace VOD.Models;

public class SignUpViewModel
{
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email address is missing or invalid.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    //[Required]
    //[DataType(DataType.Password, ErrorMessage = "Incorrect or missing password.")]
    //[Display(Name = "Confirm Password")]
    //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //public string ConfirmPassword { get; set; }
}
