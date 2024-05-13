﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class Account
    {

        public int AccNumber { get; set; }

        [ForeignKey("BrokersId")]
        [JsonIgnore]
        public Broker Brokers { get; set; }
        public int BrokersId { get; set; }

        [ForeignKey("TradersId")]
        [JsonIgnore]
        public Trader Traders { get; set; }
        public int TradersId { get; set; }

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
