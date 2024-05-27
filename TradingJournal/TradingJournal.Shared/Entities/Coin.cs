using System.Collections.Generic;

namespace TradingJournal.Shared.Entities
{
    public class Coin
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public decimal Current_price { get; set; }
        public decimal Market_cap { get; set; }
        public long Market_cap_rank { get; set; }
        public decimal Total_volume { get; set; }
        public decimal High_24h { get; set; }
        public decimal Low_24h { get; set; }
        public decimal price_change_24h { get; set; }
        public decimal price_change_percentage_24h { get; set; }
        public decimal market_cap_change_24h { get; set; }
        public decimal market_cap_change_percentage_24h { get; set; }
        public decimal circulating_supply { get; set; }
         public decimal ath { get; set; }
        public decimal atl_change_percentage { get; set; }
        public string atl_date { get; set; }
        public string last_updated { get; set; }
        public List<List<double>> Market { get; set; }
















    }
}
