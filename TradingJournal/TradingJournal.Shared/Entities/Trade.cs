using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class Trade
    {
        public int Id { get; set; }

        //Foreign Keys
        [ForeignKey("UsersId")]
        [JsonIgnore]
        public User Users { get; set; }
        public string UsersId { get; set; }

        [ForeignKey("MarketsCode")]
        [JsonIgnore]
        public Market Markets { get; set; }
        public int MarketsCode { get; set; }
    }
}
