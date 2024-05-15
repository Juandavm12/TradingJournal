//User Entitie whit all the validators
using TradingJournal.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

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

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";


    }
}
