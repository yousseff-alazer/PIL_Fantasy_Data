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
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class TeamsController : ControllerBase
    {
        private readonly fantasy_dataContext _context;

        public TeamsController(fantasy_dataContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Return List Of Team With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] TeamRequest model)
        {
            var teamResponse = new TeamResponse();
            try
            {
                model ??= new TeamRequest();
                model._context = _context;

                teamResponse = TeamService.ListTeam(model);
            }
            catch (Exception ex)
            {
                teamResponse.Message = ex.Message;
                teamResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(teamResponse);
        }



        /// <summary>
        /// Return Team With id .
        /// </summary>
        [HttpGet("GetTeamLocalize/{Teamid}", Name = "GetTeamLocalize")]
        [Produces("application/json")]
        public IActionResult GetTeamLocalize(int Teamid)
        {
            var teamLocalizeResponse = new TeamLocalizeResponse();
            try
            {
                var teamLocalizeRequest = new TeamLocalizeRequest
                {
                    _context = _context,
                    TeamLocalizeRecord = new TeamLocalizeRecord
                    {
                        TeamId = Teamid
                    }
                };
                teamLocalizeResponse = TeamLocalizeService.ListTeamLocalize(teamLocalizeRequest);
            }
            catch (Exception ex)
            {
                teamLocalizeResponse.Message = ex.Message;
                teamLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(teamLocalizeResponse);
        }

        /// <summary>
        /// Creates TeamLocalize.
        /// </summary>
        [HttpPost]
        [Route("AddTeamLocalize")]
        [Produces("application/json")]
        public IActionResult AddTeamLocalize([FromBody] TeamLocalizeRequest model)
        {
            var teamLocalizeResponse = new TeamLocalizeResponse();
            try
            {
                if (model == null)
                {
                    teamLocalizeResponse.Message = "Empty Body";
                    teamLocalizeResponse.Success = false;
                    return Ok(teamLocalizeResponse);
                }

                var editedTranslateTeam = model.TeamLocalizeRecords.Where(c => c.Id > 0).ToList();
                if (editedTranslateTeam.Count()>0)
                {
                    var editReq = new TeamLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        TeamLocalizeRecords = editedTranslateTeam
                    };
                    teamLocalizeResponse = TeamLocalizeService.EditTeamLocalize(editReq);
                }
           
                var addedTranslateTeam = model.TeamLocalizeRecords.Where(c => c.Id == 0).ToList();
                if (addedTranslateTeam.Count() > 0)
                {
                    var addReq = new TeamLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        TeamLocalizeRecords = addedTranslateTeam
                    };
                    teamLocalizeResponse = TeamLocalizeService.AddTeamLocalize(addReq);
                }
         
            }
            catch (Exception ex)
            {
                teamLocalizeResponse.Message = ex.Message;
                teamLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(teamLocalizeResponse);
        }




        /// <summary>
        /// Remove TeamLocalize .
        /// </summary>
        [HttpPost]
        [Route("DeleteTeamLocalize")]
        [Produces("application/json")]
        public IActionResult DeleteTeamLocalize([FromBody] TeamLocalizeRequest model)
        {
            var teamLocalizeResponse = new TeamLocalizeResponse();
            try
            {
                if (model == null)
                {
                    teamLocalizeResponse.Message = "Empty Body";
                    teamLocalizeResponse.Success = false;
                    return Ok(teamLocalizeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                teamLocalizeResponse = TeamLocalizeService.DeleteTeamLocalize(model);
            }
            catch (Exception ex)
            {
                teamLocalizeResponse.Message = ex.Message;
                teamLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(teamLocalizeResponse);
        }

    }
}
