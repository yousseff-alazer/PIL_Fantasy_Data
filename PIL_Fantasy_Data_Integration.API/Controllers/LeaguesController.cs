using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIL_Fantasy_Data_Integration.API.BL.Services;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;

namespace PIL_Fantasy_Data_Integration.API.Controllers
{
    [Route("api/fantasydata/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LeaguesController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public LeaguesController(fantasy_dataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Return List Of League With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] LeagueRequest model)
        {
            var leagueResponse = new LeagueResponse();
            try
            {
                model ??= new LeagueRequest();
                model._context = _context;

                leagueResponse = LeagueService.ListLeague(model);
            }
            catch (Exception ex)
            {
                leagueResponse.Message = ex.Message;
                leagueResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(leagueResponse);
        }


        /// <summary>
        /// Return League With id .
        /// </summary>
        [HttpGet("GetLeagueLocalize/{Leagueid}", Name = "GetLeagueLocalize")]
        [Produces("application/json")]
        public IActionResult GetLeagueLocalize(int Leagueid)
        {
            var leagueLocalizeResponse = new LeagueLocalizeResponse();
            try
            {
                var leagueLocalizeRequest = new LeagueLocalizeRequest
                {
                    _context = _context,
                    LeagueLocalizeRecord = new LeagueLocalizeRecord
                    {
                        LeagueId = Leagueid
                    }
                };
                leagueLocalizeResponse = LeagueLocalizeService.ListLeagueLocalize(leagueLocalizeRequest);
            }
            catch (Exception ex)
            {
                leagueLocalizeResponse.Message = ex.Message;
                leagueLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(leagueLocalizeResponse);
        }

        /// <summary>
        /// Creates LeagueLocalize.
        /// </summary>
        [HttpPost]
        [Route("AddLeagueLocalize")]
        [Produces("application/json")]
        public IActionResult AddLeagueLocalize([FromBody] LeagueLocalizeRequest model)
        {
            var leagueLocalizeResponse = new LeagueLocalizeResponse();
            try
            {
                if (model == null)
                {
                    leagueLocalizeResponse.Message = "Empty Body";
                    leagueLocalizeResponse.Success = false;
                    return Ok(leagueLocalizeResponse);
                }

                var editedTranslateLeague = model.LeagueLocalizeRecords.Where(c => c.Id > 0).ToList();
                if (editedTranslateLeague.Count() > 0)
                {
                    var editReq = new LeagueLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        LeagueLocalizeRecords = editedTranslateLeague
                    };
                    leagueLocalizeResponse = LeagueLocalizeService.EditLeagueLocalize(editReq);
                }
                var addedTranslateLeague = model.LeagueLocalizeRecords.Where(c => c.Id == 0).ToList();
                if (addedTranslateLeague.Count() > 0)
                {
                    var addReq = new LeagueLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        LeagueLocalizeRecords = addedTranslateLeague
                    };
                    leagueLocalizeResponse = LeagueLocalizeService.AddLeagueLocalize(addReq);
                }
         
            }
            catch (Exception ex)
            {
                leagueLocalizeResponse.Message = ex.Message;
                leagueLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(leagueLocalizeResponse);
        }




        /// <summary>
        /// Remove LeagueLocalize .
        /// </summary>
        [HttpPost]
        [Route("DeleteLeagueLocalize")]
        [Produces("application/json")]
        public IActionResult DeleteLeagueLocalize([FromBody] LeagueLocalizeRequest model)
        {
            var leagueLocalizeResponse = new LeagueLocalizeResponse();
            try
            {
                if (model == null)
                {
                    leagueLocalizeResponse.Message = "Empty Body";
                    leagueLocalizeResponse.Success = false;
                    return Ok(leagueLocalizeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                leagueLocalizeResponse = LeagueLocalizeService.DeleteLeagueLocalize(model);
            }
            catch (Exception ex)
            {
                leagueLocalizeResponse.Message = ex.Message;
                leagueLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(leagueLocalizeResponse);
        }
    }
}
