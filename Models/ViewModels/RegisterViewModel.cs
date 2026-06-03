using System.ComponentModel.DataAnnotations;  

namespace ForumApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        public string? UserName { get; set; } 

        [Required (AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
       

        [Required (AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? Password { get; set; }
        [Required (AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
    
}