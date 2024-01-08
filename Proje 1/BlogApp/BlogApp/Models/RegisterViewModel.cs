using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName { get; set; }
        public string? Image { get; set; }



    }
}