using System.ComponentModel.DataAnnotations;

namespace TradingJournal.Shared.DTOs
{
    public class ResetPasswordDTO
    {

        [Display(Name = "Email")]

        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]

        [Required(ErrorMessage = "The field {0} is mandatory.")]

        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]

        [Display(Name = "Password")]

        [Required(ErrorMessage = "The field {0} is mandatory.")]

        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]

        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "New password and confirmation are not the same.")]

        [DataType(DataType.Password)]

        [Display(Name = "Password Confirmation")]

        [Required(ErrorMessage = "The field {0} is mandatory.")]

        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]

        public string ConfirmPassword { get; set; } = null!;

        public string Token { get; set; } = null!;

    }

}

