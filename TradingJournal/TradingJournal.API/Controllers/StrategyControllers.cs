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
    [Route("/api/Strategies")]
    public class StrategyControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public StrategyControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Strategies.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Strategy strategy)
        {
            _context.Add(strategy);
            await _context.SaveChangesAsync();
            return Ok(strategy);
        }

        //Method Get by ID (Read)
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(int Code)
        {
            var strategy = await _context.Strategies.FirstOrDefaultAsync
                (x => x.Code == Code);

            if (strategy == null)
            {
                return NotFound();
            }
            return Ok(strategy);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Strategy strategy)
        {
            _context.Update(strategy);
            await _context.SaveChangesAsync();
            return Ok(strategy);
        }

        //Metod Delete
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(int Code)
        {
            var strategy = await _context.Strategies.FirstOrDefaultAsync
                  (x => x.Code == Code);

            if (strategy == null)
            {
                return NotFound();
            }
            _context.Remove(strategy);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Strategies.ToListAsync());
        }
    }
}
