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
    [Route("/api/Account")]
    public class AccountControllers : ControllerBase
    {
        
        private readonly DataContext _context;

        //Constructor
        public AccountControllers(DataContext context)
        {
            _context = context;
        }



        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Account account)
        {
            _context.Add(account);
            try {
            await _context.SaveChangesAsync();
            return Ok(account);
        }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("AccTypesId"))

                {
                    return BadRequest("You must chose an Account Type");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("BrokersId"))

                {
                    return BadRequest("You must chose a Broker");
                }
                else if (dbUpdateException.InnerException!.Message.Contains("UsersId"))

                {
                    return BadRequest("You must chose a Trader");
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
            try { 
            await _context.SaveChangesAsync();
            return Ok(account);
        }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("AccTypesId"))

                {
                    return BadRequest("You must chose an Account Type");
    }
                else if (dbUpdateException.InnerException!.Message.Contains("BrokersId"))

                {
                    return BadRequest("You must chose a Broker");
}
                else if (dbUpdateException.InnerException!.Message.Contains("UsersId"))

{
    return BadRequest("You must chose a Trader");
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


        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }

 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Accounts
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.AccNumber.ToString().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.AccNumber)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

{
 var queryable = _context.Accounts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.AccNumber.ToString().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
        double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
 return Ok(totalPages);
    }


}

}
