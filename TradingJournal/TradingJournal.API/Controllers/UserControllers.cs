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
    [Route("/api/Users")]
    public class UserControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public UserControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        //Method Get by ID (Read)
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync
                (x => x.Id.Equals(id) );

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        //Metod Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync
                  (x => x.Id.Equals(id));

            if (user == null)
            {
                return NotFound();
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
