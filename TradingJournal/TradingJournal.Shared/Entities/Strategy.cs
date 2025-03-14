﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TradingJournal.Shared.Entities
{
    public class Strategy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        public string Type { get; set; }

        [Display(Name = "Description")]
        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        public string Description { get; set; }

        //Reference to M*M table
        [JsonIgnore]
        public ICollection<Have> Haves { get; set; }
    }
}
