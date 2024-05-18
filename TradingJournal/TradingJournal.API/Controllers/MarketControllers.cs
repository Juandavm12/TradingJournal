using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.Shared.Entities;

namespace TradingJournal.API.Controllers
{


    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/Markets")]
    public class MarketControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public MarketControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Markets.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Market market)
        {
            _context.Add(market);
            await _context.SaveChangesAsync();
            return Ok(market);
        }

        //Method Get by ID (Read)
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(int Code)
        {
            var market = await _context.Markets.FirstOrDefaultAsync
                (x => x.Code == Code);

            if (market == null)
            {
                return NotFound();
            }
            return Ok(market);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Market market)
        {
            _context.Update(market);
            await _context.SaveChangesAsync();
            return Ok(market);
        }

        //Metod Delete
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(int Code)
        {
            var market = await _context.Markets.FirstOrDefaultAsync
                  (x => x.Code == Code);

            if (market == null)
            {
                return NotFound();
            }
            _context.Remove(market);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Markets.ToListAsync());
        }
    }

}
