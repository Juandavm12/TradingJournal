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
    [Route("/api/Traders")]
    public class TraderControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TraderControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Traders.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Trader trader)
        {
            _context.Add(trader);
            await _context.SaveChangesAsync();
            return Ok(trader);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var trader = await _context.Traders.FirstOrDefaultAsync
                (x => x.Id == id);

            if (trader == null)
            {
                return NotFound();
            }
            return Ok(trader);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Trader trader)
        {
            _context.Update(trader);
            await _context.SaveChangesAsync();
            return Ok(trader);
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var trader = await _context.Traders.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (trader == null)
            {
                return NotFound();
            }
            _context.Remove(trader);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Traders.ToListAsync());
        }

    }
}
