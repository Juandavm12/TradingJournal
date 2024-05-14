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


        [MaxLength(20, ErrorMessage = "No se permiten más de 20 dígitos")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Document { get; set; }


        [MaxLength(50, ErrorMessage = "No se permiten más de 50 Caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; }



        [MaxLength(50, ErrorMessage = "No se permiten más de 50 Caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName}{LastName}";


    }
}
