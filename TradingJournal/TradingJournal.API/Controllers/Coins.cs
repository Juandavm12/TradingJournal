using CoinpaprikaAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinpaprikaAPI.Entity;


namespace TradingJournal.API.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/Coins")]
    public class Coins
    {
          private readonly Client myClient;
        
        //Constructor
        public Coins()        {              
            myClient = new Client();            
        }

        [AllowAnonymous]
        [HttpGet]
         public async Task<List<CoinInfo>> GetCoinsAsync()
         {
             var coins = await myClient.GetCoinsAsync();
             var coinList = JsonConvert.DeserializeObject<List<CoinInfo>>(coins.Raw);
             return coinList.Take(100).ToList();
         }
         

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ExtendedCoinInfo> GetCoinByIdAsync(string id)
        {

            var coins = await myClient.GetCoinByIdAsync(id);
            var coinDetailsList = JsonConvert.DeserializeObject<ExtendedCoinInfo>(coins.Raw);
            return coinDetailsList;
        }


        [AllowAnonymous]
        [HttpGet("test2")]
        public async Task<TickerInfo> GetTickerForCoin()
        {

            var ticker = await myClient.GetTickerForIdAsync("usdt-tether");

            var tickers = JsonConvert.DeserializeObject<TickerInfo>(ticker.Raw);
            return tickers;
        }

        
    }

}
