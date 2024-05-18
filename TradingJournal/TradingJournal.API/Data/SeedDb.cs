//this class contains the methods to seed the database whit the defaults values of the entities
using System.Linq;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.API.Helpers;
using TradingJournal.Shared.Entities;
using TradingJournal.Shared.Enums;

namespace TradingJournal.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;

        }
        //Method for seed the database
        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();

            await CheckAccTypesAsync();

            await CheckBrokersAsync();

            await CheckMarketsAsync();

            await CheckStrategiesAsync();

            await CheckTraderTypesAsync();

            await CheckTradesAsync();

            await CheckHavesAsync();

            await CheckAccountsAsync(); 

            await CheckTradeLogsAsync();

            await CheckRoleAsync();

            await CheckUserAsync("98764597", "Edwin", "Ramirez", "EDWINRAMIREZGON@GMAIL.COM", "3137778067", "CARRERA 42A 78A 17",1, "https://tradingjournalphotos.blob.core.windows.net/users/5f466e68-fc01-4b23-ad34-e297afbebf90.jpg",  UserType.Admin);
            await CheckUserAsync("101715698", "Juan David", "Velasquez", "JUANDAV12@GMAIL.COM", "3005216416", "CARRERA 33 65 66", 2,"https://tradingjournalphotos.blob.core.windows.net/users/cf276c98-b4a1-4e7d-9624-f91c8c9b7166.jpg",  UserType.Admin);
        }

        //------------------  Methods indivuals by entities whit default values  ---------------//
        private async Task CheckAccountsAsync()
        {
            if (!_context.Accounts.Any())
            {
                _context.Accounts.Add(new Account
                {
                    BrokersId = 1,
                  AccTypesId = 1,
                    InitialBalance = 500000
                });

                _context.Accounts.Add(new Account
                {
                    BrokersId = 2,
                        AccTypesId = 2,
                    InitialBalance = 800000
                });

                _context.Accounts.Add(new Account
                {
                    BrokersId = 3,
                    AccTypesId = 3,
                    InitialBalance = 100000
                });

                _context.Accounts.Add(new Account
                {
                    BrokersId = 4,
                    AccTypesId = 1,
                    InitialBalance = 200000
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckAccTypesAsync()
        {
            if (!_context.AccTypes.Any())
            {
                _context.AccTypes.Add(new AccType { Name = "Standard" });
                _context.AccTypes.Add(new AccType { Name = "Zero Spread" });
                _context.AccTypes.Add(new AccType { Name = "Professional" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckBrokersAsync()
        {
            if (!_context.Brokers.Any())
            {
                _context.Brokers.Add(new Broker { Name = "Tickmill" });
                _context.Brokers.Add(new Broker { Name = "Libertex" });
                _context.Brokers.Add(new Broker { Name = "Oanda" });
                _context.Brokers.Add(new Broker { Name = "FTMO" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckHavesAsync()
        {
            if (!_context.Haves.Any())
            {
                _context.Haves.Add(new Have {  StrategiesCode = 10 });
                _context.Haves.Add(new Have { StrategiesCode = 20 });
                _context.Haves.Add(new Have { StrategiesCode = 20 });
                _ = _context.Haves.Add(new Have { StrategiesCode = 30 });
                _context.Haves.Add(new Have { StrategiesCode = 40 });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckMarketsAsync()
        {
            if (!_context.Markets.Any())
            {
                _context.Markets.Add(new Market { Code = 100, Name = "Forex" });
                _context.Markets.Add(new Market { Code = 101, Name = "Cripto Currencies" });
                _context.Markets.Add(new Market { Code = 200, Name = "Indexes" });
                _context.Markets.Add(new Market { Code = 201, Name = "Commodities" });
                _context.Markets.Add(new Market { Code = 300, Name = "Options" });
                _context.Markets.Add(new Market { Code = 301, Name = "Equities" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckStrategiesAsync()
        {
            if (!_context.Strategies.Any())
            {
                _context.Strategies.Add(new Strategy
                {
                    Code = 10,
                    Name = "Fearless",
                    Session = "Asia",
                    Type = "Position"
                });

                _context.Strategies.Add(new Strategy
                {
                    Code = 20,
                    Name = "Smart Money Concepts",
                    Session = "London",
                    Type = "Day Trading"
                });

                _context.Strategies.Add(new Strategy
                {
                    Code = 30,
                    Name = "Kill Zones",
                    Session = "New York",
                    Type = "Swing Trading"
                });

                _context.Strategies.Add(new Strategy
                {
                    Code = 40,
                    Name = "Scalping",
                    Session = "New York",
                    Type = "Short Term"
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckTradesAsync()
        {
            if (!_context.Trades.Any())
            {
                _context.Trades.Add(new Trade { MarketsCode = 100 });
                _context.Trades.Add(new Trade { MarketsCode = 200 });
                _context.Trades.Add(new Trade { MarketsCode = 300 });
                _context.Trades.Add(new Trade { MarketsCode = 301 });
                _context.Trades.Add(new Trade { MarketsCode = 101 });
                _context.Trades.Add(new Trade { MarketsCode = 201 });
                _context.Trades.Add(new Trade { MarketsCode = 301 });
                _context.Trades.Add(new Trade { MarketsCode = 100 });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckTradeLogsAsync()
        {
            if (!_context.TradeLogs.Any())
            {
                _context.TradeLogs.Add(new TradeLog
                {
                    AccountsAccNumber = 1,
                    Asset = "GBPUSD",
                    Risk = 1000,
                    Pnl = 4000,
                    Comission = 20.5
                });

                _context.TradeLogs.Add(new TradeLog
                {
                    AccountsAccNumber = 2,
                    Asset = "NAS100",
                    Risk = 5000,
                    Pnl = -5000,
                    Comission = 50
                });

                _context.TradeLogs.Add(new TradeLog
                {
                    AccountsAccNumber = 1,
                    Asset = "EURUSD",
                    Risk = 500,
                    Pnl = 1500,
                    Comission = 10.5
                });

                _context.TradeLogs.Add(new TradeLog
                {
                    AccountsAccNumber = 2,
                    Asset = "XAUUSD",
                    Risk = 3000,
                    Pnl = 10000,
                    Comission = 28
                });
            }
            await _context.SaveChangesAsync();
        }

        

        private async Task CheckTraderTypesAsync()
        {
            if (!_context.TraderTypes.Any())
            {
                _context.TraderTypes.Add(new TraderType { Name = "Swing Trader" });
                _context.TraderTypes.Add(new TraderType { Name = "Day Trader" });
                _context.TraderTypes.Add(new TraderType { Name = "Scalper" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckRoleAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstname, string lastname, string email, string phone, string address, int tradertype, string photo,  UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {

                    Document = document,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    PhoneNumber = phone,
                    UserName = email,
                    Address = address,
                    TraderTypesId = tradertype,
                    Photo = photo,
                    UserType = userType,
                   
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
            return user;
        }
    }
}
