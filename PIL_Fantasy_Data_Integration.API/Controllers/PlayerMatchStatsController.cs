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
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class PlayerMatchStatMatchStatsController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public PlayerMatchStatMatchStatsController(fantasy_dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return List Of PlayerMatchStat With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] PlayerMatchStatRequest model)
        {
            var playerMatchStatResponse = new PlayerMatchStatResponse();
            try
            {
                model ??= new PlayerMatchStatRequest();
                model._context = _context;

                playerMatchStatResponse = PlayerMatchStatService.ListPlayerMatchStat(model);
            }
            catch (Exception ex)
            {
                playerMatchStatResponse.Message = ex.Message;
                playerMatchStatResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(playerMatchStatResponse);
        }
    }
}
