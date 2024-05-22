using CoinpaprikaAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.API.Helpers;
using TradingJournal.Shared.DTOs;
using TradingJournal.Shared.Entities;

namespace TradingJournal.API.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/Coins")]
    public class Coins
    {
          private readonly CoinpaprikaAPI.Client myClient;
        
        //Constructor
        public Coins()
        {              
            myClient = new CoinpaprikaAPI.Client();
            
        }

        [AllowAnonymous]
        [HttpGet]
         public async Task<List<CoinpaprikaAPI.Entity.CoinInfo>> GetCoinListAsync()
         {
             var coins = await myClient.GetCoinsAsync();
             var coinList = JsonConvert.DeserializeObject<List<CoinpaprikaAPI.Entity.CoinInfo>>(coins.Raw);
             return coinList.Take(100).ToList();
         }

        [AllowAnonymous]
        [HttpGet("1")]
        public async Task<List<CoinpaprikaAPI.Entity.OhlcValue>> Get()
        {
           
            var coins = await myClient.GetLatestOhlcForCoinAsync("btc - bitcoin", "USD");
            var coinDetailsList = JsonConvert.DeserializeObject<List<CoinpaprikaAPI.Entity.OhlcValue>>(coins.Raw);
            return coinDetailsList.Take(100).ToList();
        }

    }

}
