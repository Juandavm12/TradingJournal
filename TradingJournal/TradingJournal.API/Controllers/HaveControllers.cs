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
    [Route("/api/Haves")]
    public class HaveControllers : ControllerBase
    {
        private readonly DataContext _context;

        //Constructor
        public HaveControllers(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Haves
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
            var queryable = _context.Haves.AsQueryable();
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
        public async Task<ActionResult> PostAsync(Have have)
        {
            _context.Add(have);
            try
            {
                if (have.UsersId != null)
                {
                    await _context.SaveChangesAsync();
                    return Ok(have);
                }
                else
                {
                    return BadRequest("You must chose a Trader");
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("Strategies"))
                {
                    return BadRequest("You must choose a strategy.");
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
            var have = await _context.Haves.FirstOrDefaultAsync
                (x => x.Id == id);

            if (have == null)
            {
                return NotFound();
            }
            return Ok(have);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Have have)
        {
            _context.Update(have);
            try
            {
                if (have.UsersId != null)
                {
                    await _context.SaveChangesAsync();
                    return Ok(have);
                }
                else
                {
                    return BadRequest("You must chose a Trader");
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("Strategies"))
                {
                    return BadRequest("You must choose a strategy.");
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
            var have = await _context.Haves.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (have == null)
            {
                return NotFound();
            }
            _context.Remove(have);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Haves.ToListAsync());
        }

    }
}
