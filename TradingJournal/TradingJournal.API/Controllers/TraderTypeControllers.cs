using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.Shared.DTOs;
using TradingJournal.Shared.Entities;
using TradingJournal.API.Helpers;

namespace TradingJournal.API.Controllers
{

  
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/TraderTypes")]
    public class TraderTypeControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TraderTypeControllers(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TraderTypes
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.Id)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.TraderTypes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
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



        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.TraderTypes.ToListAsync());
        }
    }
}
