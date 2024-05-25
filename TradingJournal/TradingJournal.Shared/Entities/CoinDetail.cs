using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class CoinDetail
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Market_cap { get; set; }
        public long Market_cap_rank { get; set; }
        public decimal Total_volume { get; set; }
        public decimal High_24h { get; set; }
        public decimal Low_24h { get; set; }
        public decimal Current_price { get; set; }
        public Description Description { get; set; }
        public Links Links { get; set; }
    }

}