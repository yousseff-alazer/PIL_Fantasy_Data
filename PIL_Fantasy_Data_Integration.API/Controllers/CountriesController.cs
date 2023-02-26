using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIL_Fantasy_Data_Integration.API.BL.Services;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;

namespace PIL_Fantasy_Data_Integration.API.Controllers
{
    [Route("api/fantasydata/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CountriesController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public CountriesController(fantasy_dataContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Return List Of Country With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] CountryRequest model)
        {
            var countryResponse = new CountryResponse();
            try
            {
                if (model == null)
                {
                    model = new CountryRequest();
                }
                model._context = _context;

                countryResponse = CountryService.ListCountry(model);
            }
            catch (Exception ex)
            {
                countryResponse.Message = ex.Message;
                countryResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(countryResponse);
        }
    }
}
