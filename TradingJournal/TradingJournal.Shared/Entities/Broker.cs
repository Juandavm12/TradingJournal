using System.ComponentModel.DataAnnotations;

namespace TradingJournal.Shared.Entities
{
    public class Broker
    {

        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Phone { get; set; }


        [Display(Name = "License Number")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string LicenseNumber { get; set; }


    }
}
