using System.ComponentModel.DataAnnotations;

namespace TradingJournal.Shared.DTOs
{
    public class ChangePasswordDTO
    {

        [DataType(DataType.Password)]

        [Display(Name = "Actual Passwordl")]

        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} dmust contain between {2} and {1} characters.")]

        [Required(ErrorMessage = "The field {0} is mandatory")]

        public string CurrentPassword { get; set; } = null!;



        [DataType(DataType.Password)]

        [Display(Name = "New password")]

        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} dmust contain between {2} and {1} characters.")]

        [Required(ErrorMessage = "The field {0} is mandatory")]

        public string NewPassword { get; set; } = null!;



        [Compare("NewPassword", ErrorMessage = "The new password and the confirmation are not the same.")]

        [DataType(DataType.Password)]

        [Display(Name = "Confirm new password")]

        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} dmust contain between {2} and {1} characters.")]

        [Required(ErrorMessage = "The field {0} is mandatory")]

        public string Confirm { get; set; } = null!;
    }
}
