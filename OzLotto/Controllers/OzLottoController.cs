using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OzLotto.Data;
using OzLotto.Models;

namespace OzLotto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OzLottoController : ControllerBase
    {
        private readonly DataContext _context;

        public OzLottoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OzLotto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsDb()
        {
          if (_context.TicketsDb == null)
          {
              return NotFound();
          }
            return await _context.TicketsDb.ToListAsync();
        }

        // GET: api/OzLotto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.TicketsDb == null)
          {
              return NotFound();
          }
            var ticket = await _context.TicketsDb.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/OzLotto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OzLotto
        [Route("QuickEntry")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketDto ticket)
        {
          if (_context.TicketsDb == null)
          {
              return Problem("Entity set 'DataContext.TicketsDb'  is null.");
          }

            var randomGenerator = new Random();
            List<int> possible = Enumerable.Range(1, 47).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                int index = randomGenerator.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            var newTicket = new Ticket
            {
                Date = DateTime.Now,
                ProductName = "Oz Lotto",
                DrawNumber = ticket.DrawNumber,
                Selection1 = listNumbers[0],
                Selection2 = listNumbers[1],
                Selection3 = listNumbers[2],
                Selection4 = listNumbers[3],
                Selection5 = listNumbers[4],
                Selection6 = listNumbers[5],
                Selection7 = listNumbers[6],
            };
            _context.TicketsDb.Add(newTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
        }

        // POST: api/OzLotto
        [Route("StandardEntry")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(TicketStd ticket)
        {
            if (_context.TicketsDb == null)
            {
                return Problem("Entity set 'DataContext.TicketsDb'  is null.");
            }

            var newTicket = new Ticket
            {
                Date = DateTime.Now,
                ProductName = "Oz Lotto",
                DrawNumber = ticket.DrawNumber,
                Selection1 = ticket.Selection1,
                Selection2 = ticket.Selection2,
                Selection3 = ticket.Selection3,
                Selection4 = ticket.Selection4,
                Selection5 = ticket.Selection5,
                Selection6 = ticket.Selection6,
                Selection7 = ticket.Selection7,
            };
            _context.TicketsDb.Add(newTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
        }

        // DELETE: api/OzLotto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.TicketsDb == null)
            {
                return NotFound();
            }
            var ticket = await _context.TicketsDb.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.TicketsDb.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.TicketsDb?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
