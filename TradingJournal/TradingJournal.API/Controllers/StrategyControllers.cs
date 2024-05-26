using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.API.Helpers;
using TradingJournal.Shared.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Strategies
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
            var queryable = _context.Strategies.AsQueryable();
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
        public async Task<ActionResult> PostAsync(Strategy strategy)
        {
            _context.Add(strategy);

            try
            {
                if (strategy.Code != 0)
                {
                    await _context.SaveChangesAsync();
                    return Ok(strategy);
                }
                else
                {
                    return BadRequest("The Strategy code can't be null!");
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("PK_Strategies"))
                {
                    return BadRequest("A Strategy with that Code already exists!.");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("Name"))
                {
                    return BadRequest("A Strategy with that Name already exists!.");
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
            try
            {

                if (strategy.Code != 0)
                {
                    await _context.SaveChangesAsync();
                    return Ok(strategy);
                }
                else
                {
                    return BadRequest("The Strategy code can't be null!");
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("PK_Strategies"))
                {
                    return BadRequest("A Strategy with that Code already exists!.");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("Name"))
                {
                    return BadRequest("A Strategy with that Name already exists!.");
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
