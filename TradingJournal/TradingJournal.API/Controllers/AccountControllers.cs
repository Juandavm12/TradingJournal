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
    [Route("/api/Account")]
    public class AccountControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public AccountControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Account account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return Ok(account);
        }

        //Method Get by ID (Read)
        [HttpGet("{AccNumber:int}")]
        public async Task<ActionResult> GetAsync(int AccNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync
                (x => x.AccNumber == AccNumber);

            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Account account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
            return Ok(account);
        }

        //Metod Delete
        [HttpDelete("{AccNumber:int}")]
        public async Task<ActionResult> DeleteAsync(int AccNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync
                  (x => x.AccNumber == AccNumber);

            if (account == null)
            {
                return NotFound();
            }
            _context.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

               
    }
}
