using Microsoft.EntityFrameworkCore;
using TradingJournal.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradingJournal.API.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccType> AccTypes { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Have> Haves { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Strategy> Strategys { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeLog> TradeLogs { get; set; }
        public DbSet<Trader> Traders { get; set; }
        public DbSet<TraderType> TraderType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
