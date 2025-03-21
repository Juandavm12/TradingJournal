﻿//User Entitie whit all the validators
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TradingJournal.Shared.Enums;

namespace TradingJournal.Shared.Entities
{
    //inheritance whit the IdentityUser
    public class User : IdentityUser
    {



        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Document { get; set; }


        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string FirstName { get; set; }



        [MaxLength(50, ErrorMessage = "More than 50 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }
        public UserType UserType { get; set; }

        [ForeignKey("TraderTypesId")]
        [JsonIgnore]
        public TraderType TraderTypes { get; set; }
        public int TraderTypesId { get; set; }

        //Reference to M*M table
        [JsonIgnore]
        public ICollection<Have> Haves { get; set; }

        [JsonIgnore]
        public ICollection<Trade> Trades { get; set; }


        public string FullName => $"{FirstName} {LastName}";



    }
}
