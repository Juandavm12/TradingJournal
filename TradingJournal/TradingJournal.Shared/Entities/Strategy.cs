using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class Strategy
    {
        public int Code { get; set; }

        [Display(Name = "Name")]
        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

        [Display(Name = "Session")]
        [MaxLength(8, ErrorMessage = "More than 8 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Session { get; set; }

        [Display(Name = "Strategy Type")]
        [MaxLength(10, ErrorMessage = "More than 10 charachters are not allowed")]
        public string Type { get; set; }

        [Display(Name = "Description")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Description { get; set; }

        //Reference to M*M table
        [JsonIgnore]
        public ICollection<Have> Haves { get; set; }
    }
}
