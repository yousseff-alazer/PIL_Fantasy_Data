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
    public class CountriesController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public CountriesController(fantasy_dataContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }
    }
}
