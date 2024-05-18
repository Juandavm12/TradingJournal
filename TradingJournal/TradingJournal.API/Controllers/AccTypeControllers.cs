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
    [Route("/api/AccTypes")]
    public class AccTypeControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public AccTypeControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.AccTypes.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(AccType acctype)
        {
            _context.Add(acctype);
            await _context.SaveChangesAsync();
            return Ok(acctype);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var acctype = await _context.AccTypes.FirstOrDefaultAsync
                (x => x.Id == id);

            if (acctype == null)
            {
                return NotFound();
            }
            return Ok(acctype);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(AccType acctype)
        {
            _context.Update(acctype);
            await _context.SaveChangesAsync();
            return Ok(acctype);
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var acctype = await _context.AccTypes.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (acctype == null)
            {
                return NotFound();
            }
            _context.Remove(acctype);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.AccTypes.ToListAsync());
        }
    }
}
