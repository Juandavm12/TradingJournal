﻿using System.ComponentModel.DataAnnotations;

namespace TradingJournal.Shared.Entities
{
    public class AccType
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

    }
}
