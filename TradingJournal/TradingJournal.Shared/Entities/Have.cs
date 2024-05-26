using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TradingJournal.Shared.Entities
{
    public class Have
    {
        public int Id { get; set; }

        //Foreign Keys
        [ForeignKey("UsersId")]
        [JsonIgnore]
        public User Users { get; set; }
        public string UsersId { get; set; }

        [ForeignKey("StrategiesCode")]
        [JsonIgnore]
        public Strategy Strategies { get; set; }
        public int StrategiesCode { get; set; }
    }
}
