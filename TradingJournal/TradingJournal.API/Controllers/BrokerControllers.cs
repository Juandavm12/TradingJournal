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
    [Route("/api/BrokerControllers")]
    public class BrokerControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public BrokerControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Brokers.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Broker broker)
        {
            _context.Add(broker);
            await _context.SaveChangesAsync();
            return Ok(broker);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var broker = await _context.Brokers.FirstOrDefaultAsync
                (x => x.Id == id);

            if (broker == null)
            {
                return NotFound();
            }
            return Ok(broker);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Broker broker)
        {
            _context.Update(broker);
            await _context.SaveChangesAsync();
            return Ok(broker);
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var broker = await _context.Brokers.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (broker == null)
            {
                return NotFound();
            }
            _context.Remove(broker);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
