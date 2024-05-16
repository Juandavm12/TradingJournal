using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.Shared.Entities;

namespace TradingJournal.API.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("/api/Trades")]
    public class TradeControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TradeControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Trades.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Trade trade)
        {
            _context.Add(trade);
            await _context.SaveChangesAsync();
            return Ok(trade);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync
                (x => x.Id == id);

            if (trade == null)
            {
                return NotFound();
            }
            return Ok(trade);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Trade trade)
        {
            _context.Update(trade);
            await _context.SaveChangesAsync();
            return Ok(trade);
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (trade == null)
            {
                return NotFound();
            }
            _context.Remove(trade);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Trades.ToListAsync());
        }
    }
}
