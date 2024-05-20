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
    [Route("/api/Markets")]
    public class MarketControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public MarketControllers(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Markets
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.Code)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.Markets.AsQueryable();
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
        public async Task<ActionResult> PostAsync(Market market)
        {
            _context.Add(market);
            try
            {
                await _context.SaveChangesAsync();
            return Ok(market);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("PK_Markets"))
                {
                    return BadRequest("A Market with that Code already exists!.");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("Name"))
                {
                    return BadRequest("A Market with that Name already exists!.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Method Get by ID (Read)
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(int Code)
        {
            var market = await _context.Markets.FirstOrDefaultAsync
                (x => x.Code == Code);

            if (market == null)
            {
                return NotFound();
            }
            return Ok(market);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Market market)
        {
            _context.Update(market);
            try { 
            await _context.SaveChangesAsync();
            return Ok(market);
        }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("PK_Markets"))
                {
                    return BadRequest("A Market with that Code already exists!.");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("Name"))
                {
                    return BadRequest("A Market with that Name already exists!.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Metod Delete
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(int Code)
        {
            var market = await _context.Markets.FirstOrDefaultAsync
                  (x => x.Code == Code);

            if (market == null)
            {
                return NotFound();
            }
            _context.Remove(market);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Markets.ToListAsync());
        }
    }

}
