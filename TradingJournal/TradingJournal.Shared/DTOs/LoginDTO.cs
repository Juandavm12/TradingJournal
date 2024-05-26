using System.ComponentModel.DataAnnotations;

namespace TradingJournal.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MinLength(6, ErrorMessage = "The {0} field must be at least {1} characters.")]
        public string Password { get; set; } = null!;

    }
}
