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
    [Route("api/fantasydata/[controller]")]
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

    }
}
