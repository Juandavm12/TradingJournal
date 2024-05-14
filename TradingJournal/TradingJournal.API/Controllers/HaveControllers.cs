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
    [Route("/api/HaveControllers")]
    public class HaveControllers : ControllerBase
    {
        private readonly DataContext _context;

        //Constructor
        public HaveControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Haves.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Have have)
        {
            _context.Add(have);
            await _context.SaveChangesAsync();
            return Ok(have);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var have = await _context.Haves.FirstOrDefaultAsync
                (x => x.Id == id);

            if (have == null)
            {
                return NotFound();
            }
            return Ok(have);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Have have)
        {
            _context.Update(have);
            await _context.SaveChangesAsync();
            return Ok(have);
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var have = await _context.Haves.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (have == null)
            {
                return NotFound();
            }
            _context.Remove(have);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
