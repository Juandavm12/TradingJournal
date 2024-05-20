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
    [Route("/api/Trades")]
    public class TradeControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TradeControllers(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Trades
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.Id)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.Trades.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Trade trade)
        {
            _context.Add(trade);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(trade);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("Markets"))
                {
                    return BadRequest("You must choose a Market.");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("UsersId"))
                {
                    return BadRequest("You must choose a Trader.");
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync
                (x => x.Id == id);

            if (trade == null)
            {
                return NotFound();
            }
            return Ok(trade);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Trade trade)
        {
            _context.Update(trade);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(trade);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("Markets"))
                {
                    return BadRequest("You must choose a Market.");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("UsersId"))
                {
                    return BadRequest("You must choose a Trader.");
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
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (trade == null)
            {
                return NotFound();
            }
            _context.Remove(trade);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Trades.ToListAsync());
        }
    }
}
