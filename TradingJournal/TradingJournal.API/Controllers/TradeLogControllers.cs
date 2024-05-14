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
    [Route("/api/TradeLogControllers")]
    public class TradeLogControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TradeLogControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.TradeLogs.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(TradeLog tradelog)
        {
            _context.Add(tradelog);
            await _context.SaveChangesAsync();
            return Ok(tradelog);
        }

        //Method Get by ID (Read)
        [HttpGet("{TradeNumber:int}")]
        public async Task<ActionResult> GetAsync(int TradeNumber)
        {
            var tradelog = await _context.TradeLogs.FirstOrDefaultAsync
                (x => x.TradeNumber == TradeNumber);

            if (tradelog == null)
            {
                return NotFound();
            }
            return Ok(tradelog);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(TradeLog tradelog)
        {
            _context.Update(tradelog);
            await _context.SaveChangesAsync();
            return Ok(tradelog);
        }

        //Metod Delete
        [HttpDelete("{TradeNumber:int}")]
        public async Task<ActionResult> DeleteAsync(int TradeNumber)
        {
            var tradelog = await _context.TradeLogs.FirstOrDefaultAsync
                  (x => x.TradeNumber == TradeNumber);

            if (tradelog == null)
            {
                return NotFound();
            }
            _context.Remove(tradelog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
