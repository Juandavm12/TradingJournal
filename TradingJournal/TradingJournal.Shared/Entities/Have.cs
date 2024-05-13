using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class Have
    {
        public int Id { get; set; }

        //Foreign Keys
        [ForeignKey("TradersId")]
        [JsonIgnore]
        public Trader Traders { get; set; }
        public int TradersId { get; set; }

        [ForeignKey("StrategiesId")]
        [JsonIgnore]
        public Strategy Strategies { get; set; }
        public int StrategiesId { get; set; }
    }
}
