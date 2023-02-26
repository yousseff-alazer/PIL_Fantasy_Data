using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class PlayersController : ControllerBase
    {
        private readonly fantasy_dataContext _context;
        private readonly ILogger<PlayersController> _logger;
        private readonly IConfiguration _config;
        //private readonly IDistributedCache _cache;
        public PlayersController(fantasy_dataContext context, ILogger<PlayersController> logger, IConfiguration config/*, IDistributedCache cache*/)
        {
            _config = config;
            _logger = logger;
            _context = context;
            //_cache = cache;
        }
        /// <summary>
        /// Return List Of Player With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] PlayerRequest model)
        {
            var playerResponse = new PlayerResponse();
            try
            {
                model ??= new PlayerRequest();
                model._context = _context;

                playerResponse = PlayerService.ListPlayer(model);
            }
            catch (Exception ex)
            {
                playerResponse.Message = ex.Message;
                playerResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(playerResponse);
        }

        /// <summary>
        /// Return List Of Player by position With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFilteredByPosition")]
        [Produces("application/json")]
        public IActionResult GetFilteredByPosition([FromBody] PlayerRequest model)
        {
            var playerResponse = new PlayerResponse();
            try
            {
                model ??= new PlayerRequest();
                model._context = _context;
                if (model.PlayerRecord == null)
                    model.PlayerRecord = new PlayerRecord();
                model.PlayerRecord.PositionFilter = true;
                //var cacheKey = string.Empty;
                //if (model.PlayerRecord.TeamsIds.Count > 0)
                //{
                //    cacheKey = $"players_{model.PlayerRecord.TeamsIds[0]}_{model.PlayerRecord.TeamsIds[1]}_{DateTime.UtcNow.Date.ToShortDateString()}";
                //}
                //var cachedData = _cache.Get(cacheKey);
                //var cachedData = null;
                //if (cachedData != null)
                //{
                //    // If data found in cache, encode and deserialize cached data
                //    var cachedDataString = Encoding.UTF8.GetString(cachedData);
                //    playerResponse = JsonConvert.DeserializeObject<PlayerResponse>(cachedDataString);
                //}
                //else
                //{
                    playerResponse = PlayerService.ListPlayer(model);
                    // serialize data
                    //var cachedDataString = JsonConvert.SerializeObject(playerResponse);
                    //var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                    //// set cache options 
                    //var options = new DistributedCacheEntryOptions()
                    //    .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(12));

                    //// Add data in cache
                    //_cache.Set(cacheKey, newDataToCache, options);
                //}
                
           
            }
            catch (Exception ex)
            {
                playerResponse.Message = ex.Message;
                playerResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(playerResponse);
        }



        /// <summary>
        /// Edit Player with id.
        /// </summary>
        [HttpPost]
        [Route("Edit")]
        [Produces("application/json")]
        public IActionResult Edit([FromBody] PlayerRequest model)
        {
            var playerResponse = new PlayerResponse();
            try
            {
                model ??= new PlayerRequest();
                model._context = _context;

                playerResponse = PlayerService.EditPlayer(model);
            }
            catch (Exception ex)
            {
                playerResponse.Message = ex.Message;
                playerResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(playerResponse);
        }



        /// <summary>
        /// Return Player With id .
        /// </summary>
        [HttpGet("GetPlayerLocalize/{Playerid}", Name = "GetPlayerLocalize")]
        [Produces("application/json")]
        public IActionResult GetPlayerLocalize(int Playerid)
        {
            var playerLocalizeResponse = new PlayerLocalizeResponse();
            try
            {
                var playerLocalizeRequest = new PlayerLocalizeRequest
                {
                    _context = _context,
                    PlayerLocalizeRecord = new PlayerLocalizeRecord
                    {
                        PlayerId = Playerid
                    }
                };
                playerLocalizeResponse = PlayerLocalizeService.ListPlayerLocalize(playerLocalizeRequest);
            }
            catch (Exception ex)
            {
                playerLocalizeResponse.Message = ex.Message;
                playerLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(playerLocalizeResponse);
        }

        /// <summary>
        /// Creates PlayerLocalize.
        /// </summary>
        [HttpPost]
        [Route("AddPlayerLocalize")]
        [Produces("application/json")]
        public IActionResult AddPlayerLocalize([FromBody] PlayerLocalizeRequest model)
        {
            var playerLocalizeResponse = new PlayerLocalizeResponse();
            try
            {
                if (model == null)
                {
                    playerLocalizeResponse.Message = "Empty Body";
                    playerLocalizeResponse.Success = false;
                    return Ok(playerLocalizeResponse);
                }

                var editedTranslatePlayer = model.PlayerLocalizeRecords.Where(c => c.Id > 0).ToList();
                if (editedTranslatePlayer.Count() > 0)
                {
                    var editReq = new PlayerLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        PlayerLocalizeRecords = editedTranslatePlayer
                    };
                    playerLocalizeResponse = PlayerLocalizeService.EditPlayerLocalize(editReq);

                }
                var addedTranslatePlayer = model.PlayerLocalizeRecords.Where(c => c.Id == 0).ToList();
                if (addedTranslatePlayer.Count() > 0)
                {
                    var addReq = new PlayerLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        PlayerLocalizeRecords = addedTranslatePlayer
                    };
                    playerLocalizeResponse = PlayerLocalizeService.AddPlayerLocalize(addReq);
                }
            
            }
            catch (Exception ex)
            {
                playerLocalizeResponse.Message = ex.Message;
                playerLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(playerLocalizeResponse);
        }




        /// <summary>
        /// Remove PlayerLocalize .
        /// </summary>
        [HttpPost]
        [Route("DeletePlayerLocalize")]
        [Produces("application/json")]
        public IActionResult DeletePlayerLocalize([FromBody] PlayerLocalizeRequest model)
        {
            var playerLocalizeResponse = new PlayerLocalizeResponse();
            try
            {
                if (model == null)
                {
                    playerLocalizeResponse.Message = "Empty Body";
                    playerLocalizeResponse.Success = false;
                    return Ok(playerLocalizeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                playerLocalizeResponse = PlayerLocalizeService.DeletePlayerLocalize(model);
            }
            catch (Exception ex)
            {
                playerLocalizeResponse.Message = ex.Message;
                playerLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(playerLocalizeResponse);
        }

    }
}
