using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        // [Required]
        // [EmailAddress]
        // public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        // [Required]
        // [StringLength(10, MinimumLength = 6, ErrorMessage = "6 ile 10 karakter arasında şifre giriniz")] // maximum 10 karakter
        // [DataType(DataType.Password)]
        // public string? Password { get; set; }



    }
}