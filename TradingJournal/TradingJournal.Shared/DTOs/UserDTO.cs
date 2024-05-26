using System.ComponentModel.DataAnnotations;
using TradingJournal.Shared.Entities;
namespace TradingJournal.Shared.DTOs
{
    public class UserDTO : User
    {

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must be between {2} and {1} characters")]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "Password and confirmation are not the same")]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must be between {2} and {1} characters")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
