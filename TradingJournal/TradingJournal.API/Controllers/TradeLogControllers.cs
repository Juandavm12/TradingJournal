﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Route("/api/TradeLogs")]
    public class TradeLogControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public TradeLogControllers(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TradeLogs
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.TradeNumber.ToString().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.TradeNumber)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.TradeLogs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.TradeNumber.ToString().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(TradeLog tradelog)
        {
            _context.Add(tradelog);
            try
            {
                if (tradelog.Risk == 0)
                {

                    return BadRequest("You need to Risk something to gain");
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(tradelog);
                }
            }

            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("AccountsAccNumber"))
                {
                    return BadRequest("You must chose a Account Number");
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
        [HttpGet("{TradeNumber:int}")]
        public async Task<ActionResult> GetAsync(int TradeNumber)
        {
            var tradelog = await _context.TradeLogs.FirstOrDefaultAsync
                (x => x.TradeNumber == TradeNumber);

            if (tradelog == null)
            {
                return NotFound();
            }
            return Ok(tradelog);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(TradeLog tradelog)
        {
            _context.Update(tradelog);
            try
            {
                if (tradelog.Risk == 0
                )
                {

                    return BadRequest("You need to Risk something to gain");
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(tradelog);
                }
            }

            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("AccountsAccNumber"))
                {
                    return BadRequest("You must chose a Account Number");
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
        [HttpDelete("{TradeNumber:int}")]
        public async Task<ActionResult> DeleteAsync(int TradeNumber)
        {
            var tradelog = await _context.TradeLogs.FirstOrDefaultAsync
                  (x => x.TradeNumber == TradeNumber);

            if (tradelog == null)
            {
                return NotFound();
            }
            _context.Remove(tradelog);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.TradeLogs.ToListAsync());
        }
    }
}
