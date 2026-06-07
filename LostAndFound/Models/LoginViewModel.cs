using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models
{
    public class LoginViewModel
    {

        [Required]
        [RegularExpression(
            @"^\d{4}-\d{5}-[A-Z]{2}-\d$",
            ErrorMessage = "Student Number must be in format 0000-00000-XX-0")]
        public string StudentNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}