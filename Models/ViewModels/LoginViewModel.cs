using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        public string? UserName { get; set; } 

        [Required (AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; } 
    }
}