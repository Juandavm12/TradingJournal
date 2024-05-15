using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class Trader
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Address { get; set; }

        [Display(Name = "CellNumber")]
        [MaxLength(10, ErrorMessage = "More than 10 charachters are not allowed")]
        public string CellNumber { get; set; }

        //Trader Types FK 
        [ForeignKey("TraderTypesId")]
        [JsonIgnore]
        public TraderType TraderTypes { get; set; }
        public int TraderTypesId { get; set; }

        //Reference to M*M table
        [JsonIgnore]
        public ICollection<Have> Haves { get; set; }

        [JsonIgnore]
        public ICollection<Trade> Trades { get; set; }
    }
}

