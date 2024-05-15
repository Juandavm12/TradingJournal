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
    [Route("/api/TraderTypes")]
    public class TraderTypeControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TraderTypeControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.TraderTypes.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(TraderType tradertype)
        {
            _context.Add(tradertype);
            await _context.SaveChangesAsync();
            return Ok(tradertype);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var tradertype = await _context.TraderTypes.FirstOrDefaultAsync
                (x => x.Id == id);

            if (tradertype == null)
            {
                return NotFound();
            }
            return Ok(tradertype);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(TraderType tradertype)
        {
            _context.Update(tradertype);
            await _context.SaveChangesAsync();
            return Ok(tradertype);
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var tradertype = await _context.TraderTypes.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (tradertype == null)
            {
                return NotFound();
            }
            _context.Remove(tradertype);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
