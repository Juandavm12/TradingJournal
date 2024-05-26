using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
