using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
namespace PIL_Fantasy_Data_Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerMatchRatingsController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public PlayerMatchRatingsController(fantasy_dataContext context)
        {
            _context = context;
        }

        // GET: api/PlayerMatchRatings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerMatchRating>>> GetPlayerMatchRatings()
        {
            return await _context.PlayerMatchRatings.ToListAsync();
        }

        // GET: api/PlayerMatchRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerMatchRating>> GetPlayerMatchRating(long id)
        {
            var playerMatchRating = await _context.PlayerMatchRatings.FindAsync(id);

            if (playerMatchRating == null)
            {
                return NotFound();
            }

            return playerMatchRating;
        }

        // PUT: api/PlayerMatchRatings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerMatchRating(long id, PlayerMatchRating playerMatchRating)
        {
            if (id != playerMatchRating.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerMatchRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerMatchRatingExists(id))
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

        // POST: api/PlayerMatchRatings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerMatchRating>> PostPlayerMatchRating(PlayerMatchRating playerMatchRating)
        {
            _context.PlayerMatchRatings.Add(playerMatchRating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerMatchRating", new { id = playerMatchRating.Id }, playerMatchRating);
        }

        // DELETE: api/PlayerMatchRatings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerMatchRating(long id)
        {
            var playerMatchRating = await _context.PlayerMatchRatings.FindAsync(id);
            if (playerMatchRating == null)
            {
                return NotFound();
            }

            _context.PlayerMatchRatings.Remove(playerMatchRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerMatchRatingExists(long id)
        {
            return _context.PlayerMatchRatings.Any(e => e.Id == id);
        }
    }
}
