using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TradingJournal.Shared.Entities
{
    public class Account
    {
        [Key]
        public int AccNumber { get; set; }

        [ForeignKey("BrokersId")]
        [JsonIgnore]
        public Broker Brokers { get; set; }
        public int BrokersId { get; set; }

        [ForeignKey("UsersId")]
        [JsonIgnore]
        public User Users { get; set; }
        public string UsersId { get; set; }

        [ForeignKey("AccTypesId")]
        [JsonIgnore]
        public AccType AccTypes { get; set; }
        public int AccTypesId { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "InitialBalance")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public double InitialBalance { get; set; }
    }
}
