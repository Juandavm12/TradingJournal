using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TradingJournal.Shared.Entities;

namespace TradingJournal.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccType> AccTypes { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Have> Haves { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Strategy> Strategies { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeLog> TradeLogs { get; set; }
        public DbSet<TraderType> TraderTypes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccType>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Broker>().HasIndex(c => c.LicenseNumber).IsUnique();
            modelBuilder.Entity<Market>().HasIndex(c => c.Code).IsUnique();
            modelBuilder.Entity<Market>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Strategy>().HasIndex(c => c.Code).IsUnique();
            modelBuilder.Entity<Strategy>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<TraderType>().HasIndex(c => c.Name).IsUnique();

        }
    }
}
