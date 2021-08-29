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
    public class MatchesController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public MatchesController(fantasy_dataContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches.ToListAsync();
        }

    }
}
