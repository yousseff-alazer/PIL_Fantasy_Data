using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json.Linq;
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
    public class MatchesController : ControllerBase
    {
        private readonly fantasy_dataContext _context;
        private readonly ILogger<MatchesController> _logger;
        private readonly IConfiguration _config;
        //private readonly IDistributedCache _cache;
        public MatchesController(fantasy_dataContext context, ILogger<MatchesController> logger, IConfiguration config/*, IDistributedCache cache*/)
        {
            _config = config;
            _logger = logger;
            _context = context;
            //_cache = cache;
        }

        /// <summary>
        /// Return List Of Match With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] MatchRequest model)
        {
            var matchResponse = new MatchResponse();
            try
            {
                model ??= new MatchRequest();
                model._context = _context;
                // Get data from cache
                var cacheKey = $"matches_{DateTime.UtcNow.Date.ToShortDateString()}";
                if (model.MatchRecord != null &&
                    model.MatchRecord.Current != null
                    && model.MatchRecord.Current.Value)
                {
                    if (model.MatchRecord.LeagueId > 0)
                    {
                        cacheKey = $"matches_{model.MatchRecord.LeagueId}_{DateTime.UtcNow.Date.ToShortDateString()}";
                    }
                    else
                    {
                        cacheKey = $"matches_current_{DateTime.UtcNow.Date.ToShortDateString()}";
                    }
                }
                //var cachedData = _cache.Get(cacheKey);
                //if (cachedData != null)
                //{
                //    // If data found in cache, encode and deserialize cached data
                //    var cachedDataString = Encoding.UTF8.GetString(cachedData);
                //    matchResponse = JsonConvert.DeserializeObject<MatchResponse>(cachedDataString);
                //}
                //else
                //{
                    //If not found, then fetch data from database
                    matchResponse = MatchService.ListMatch(model);
                    HandleMatchReponseWithPrize(matchResponse);
                    // serialize data
                    //var cachedDataString = JsonConvert.SerializeObject(matchResponse);
                    //var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                    //// set cache options 
                    //var options = new DistributedCacheEntryOptions()
                    //    .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(12));

                    // Add data in cache
                    //_cache.Set(cacheKey, newDataToCache, options);
                //}
            }
            catch (Exception ex)
            {
                matchResponse.Message = ex.Message;
                matchResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(matchResponse);
        }

        /// <summary>
        /// Return List Of Match With current .
        /// </summary>
        [HttpGet]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered(bool current)
        {
            var matchResponse = new MatchResponse();
            try
            {
                var model = new MatchRequest();
                model ??= new MatchRequest();
                model.MatchRecord = new MatchRecord
                {
                    Current=current
                };
                model._context = _context;
                matchResponse = MatchService.ListMatch(model);
            }
            catch (Exception ex)
            {
                matchResponse.Message = ex.Message;
                matchResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(matchResponse);
        }

        [HttpGet]
        [Route("Get")]
       // [Produces("application/json")]
        public IActionResult Get()
        {
            var matchResponse = new MatchResponse();
            try
            {
                var model = new MatchRequest();
                model ??= new MatchRequest();
                model.MatchRecord = new MatchRecord
                {
                    Current = true
                };
                model._context = _context;
                matchResponse = MatchService.ListMatch(model);
                if (matchResponse.Success&&matchResponse.MatchRecords.Count()>0)
                {
                    return Ok(matchResponse);
                }
            }
            catch (Exception ex)
            {
                matchResponse.Message = ex.Message;
                matchResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(ex.Message);
            }

            return Ok("Done");
        }
        private void HandleMatchReponseWithPrize(MatchResponse matchResponse)
        {
            if (matchResponse.Success && matchResponse.MatchRecords.Count > 0)
            {
                var MatchIds = matchResponse.MatchRecords.Select(c => c.Id).ToList();
                var matchContestPrizeRecord = new MatchContestPrizeRecord();
                matchContestPrizeRecord.Ids = MatchIds;
                var jsonString = JsonConvert.SerializeObject(matchContestPrizeRecord, Formatting.None);
                string contestUrl = _config.GetValue<string>("Url:Contest");
                string userName = _config.GetValue<string>("Secret:UserName");
                string password = _config.GetValue<string>("Secret:Password");
                var url = contestUrl + "grand/matches";
                var httpResponse =
                    UIHelper.AddRequestToServiceApi(
                        url, jsonString,userName,password);
                var responseResult = httpResponse.Content.ReadAsStringAsync().Result;
                var contestPrizeRes = JsonConvert.DeserializeObject<Rbase>(responseResult);
                if (contestPrizeRes.Data.Count() > 0)
                {
                    var desired = (from b in matchResponse.MatchRecords
                                   join a in contestPrizeRes.Data on b.Id.ToString() equals a.Id into joined
                                   from j in joined.DefaultIfEmpty()
                                   select new MatchRecord
                                   {
                                       Id = b.Id,
                                       IntegrationId = b.IntegrationId,
                                       Team1Id = b.Team1Id,
                                       Team2Id = b.Team2Id,
                                       StartDatetime = b.StartDatetime,
                                       EndDatetime = b.EndDatetime,
                                       ModifiedBy = b.ModifiedBy,
                                       Status = b.Status,
                                       Team1Score = b.Team1Score,
                                       Team2Score = b.Team2Score,
                                       LeagueId = b.LeagueId,
                                       Team1ImageUrl = b.Team1ImageUrl,
                                       Team2ImageUrl = b.Team2ImageUrl,
                                       Week = b.Week,
                                       HomeTeamId = b.HomeTeamId,
                                       CreationDate = b.CreationDate,
                                       Team1IntegrationId = b.Team1IntegrationId,
                                       Team2IntegrationId = b.Team2IntegrationId,
                                       LeagueIntegrationId = b.LeagueIntegrationId,
                                       LeagueDisplayOrder = b.LeagueDisplayOrder,
                                       Title = b.Title,
                                       Description = b.Description,
                                       Team1Name = b.Team1Name,
                                       Team2Name = b.Team2Name,
                                       LeagueImageUrl = b.LeagueImageUrl,
                                       LeagueName = b.LeagueName,
                                       IsSync = b.IsSync,
                                       PlayerMatchRatings = b.PlayerMatchRatings,
                                       Name = b.Name,
                                       Prize = j != null ? j.Prize : null
                                   }).ToList();
                    matchResponse.MatchRecords = desired;
                }

            }
        }

        /// <summary>
        /// SendEndMatch .
        /// </summary>
        [HttpGet]
        [Route("SendEndMatch")]
        [Produces("application/json")]
        public IActionResult SendEndMatch()
        {
            var matchResponse = new MatchResponse();
            var jsonString = string.Empty;
            var responseResult = string.Empty;
            try
            {
                
                //LogHelper.LogInfo("Test Log");
                // _logger.LogInformation("Test Log");
                var model = new MatchRequest();

                model._context = _context;
                model.MatchRecord = new Fantasy_Data.CommonDefinitions.Records.MatchRecord();
                model.MatchRecord.EndToday = true;
                model.MatchRecord.IsSync = false;
                //model.MatchRecord.Id = 760;
                matchResponse = MatchService.ListMatch(model);
                if (matchResponse.Success&&matchResponse.MatchRecords.Count>0)
                {
                    foreach (var matchRecord in matchResponse.MatchRecords)
                    {
                        //ToDo send to nagy 
                        try 
                        {
                            jsonString = JsonConvert.SerializeObject(matchRecord, Formatting.None);

                            string fantasyTeams = _config.GetValue<string>("Url:FantasyTeams");
                            var url = fantasyTeams + "FantasyTeams/EndMatch";
                            string userName = _config.GetValue<string>("Secret:UserName");
                            string password = _config.GetValue<string>("Secret:Password");
                            _logger.LogInformation(url);
                            _logger.LogInformation(jsonString);
                            var httpResponse =
                                UIHelper.AddRequestToServiceApi(
                                    url, jsonString, userName, password);
                             responseResult = httpResponse.Content.ReadAsStringAsync().Result;
                            _logger.LogInformation(responseResult);
                        }
                        catch (Exception ex)
                        {
                            matchResponse.Message = ex.Message;
                            matchResponse.Success = false;
                            _logger.LogInformation(ex.Message + ex.StackTrace);
                            return Ok(ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                matchResponse.Message = ex.Message;
                matchResponse.Success = false;
                _logger.LogInformation(ex.Message+ ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }
            //_logger.LogInformation( _context.Database.GetDbConnection().ConnectionString);
            return Ok(/*jsonString + " " +*/ responseResult);
        }

        /// <summary>
        /// Receive EndMatch Sync.
        /// </summary>
        [HttpGet]
        [Route("SyncMatch")]
        [Produces("application/json")]
        public IActionResult SyncMatch(string matchid)
        {
            try {
                if (!string.IsNullOrWhiteSpace(matchid))
                {
                    var id = Convert.ToInt64(matchid);
                    var match = _context.Matches.Find(id);
                    match.IsSync = true;
                   var status=_context.SaveChanges();
                    return Ok(status);
                }
                else
                {
                    return BadRequest("Empty match id");
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }
        }

        //        POST : Game Postponed
        //CANC : Game Cancelled
        //SUSP : Game Suspended

        /// <summary>
        /// Receive NotPlayedMatch
        /// </summary>
        [HttpGet]
        [Route("NotPlayedMatch")]
        [Produces("application/json")]
        public IActionResult NotPlayedMatch()
        {
            var matchResponse = new MatchResponse();
            var jsonString = string.Empty;
            var responseResult = string.Empty;
            try
            {

                //LogHelper.LogInfo("Test Log");
                // _logger.LogInformation("Test Log");
                var model = new MatchRequest();

                model._context = _context;
                model.MatchRecord = new Fantasy_Data.CommonDefinitions.Records.MatchRecord();
                model.MatchRecord.NotPlayed = true;
                model.MatchRecord.IsSync = false;
                //model.MatchRecord.Id = 760;
                matchResponse = MatchService.ListMatch(model);
                //ToDo send to nagy 
                if (matchResponse.Success && matchResponse.MatchRecords.Count > 0)
                {
                    foreach (var matchRecord in matchResponse.MatchRecords)
                    {
                        try
                        {
                            jsonString = JsonConvert.SerializeObject(matchRecord, Formatting.None);

                            string fantasyTeams = _config.GetValue<string>("Url:FantasyTeams");
                            var url = fantasyTeams + "FantasyTeams/EndMatch";
                            string userName = _config.GetValue<string>("Secret:UserName");
                            string password = _config.GetValue<string>("Secret:Password");
                            _logger.LogInformation(url);
                            _logger.LogInformation(jsonString);
                            var httpResponse =
                                UIHelper.AddRequestToServiceApi(
                                    url, jsonString, userName, password);
                            responseResult = httpResponse.Content.ReadAsStringAsync().Result;
                            _logger.LogInformation(responseResult);
                        }
                        catch (Exception ex)
                        {
                            matchResponse.Message = ex.Message;
                            matchResponse.Success = false;
                            _logger.LogInformation(ex.Message + ex.StackTrace);
                            return Ok(ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                matchResponse.Message = ex.Message;
                matchResponse.Success = false;
                _logger.LogInformation(ex.Message + ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }
            //_logger.LogInformation( _context.Database.GetDbConnection().ConnectionString);
            return Ok(/*jsonString + " " +*/ responseResult);
        }
        /// <summary>
        /// MatchStart Sync.
        /// </summary>
        [HttpGet]
        [Route("MatchStart")]
        [Produces("application/json")]
        public IActionResult MatchStart(long matchid)
        {
            var matchResponse = new MatchResponse();
            try
            {
                //LogHelper.LogInfo("Test Log");
                // _logger.LogInformation("Test Log");
                var model = new MatchRequest();

                model._context = _context;
                model.MatchRecord = new Fantasy_Data.CommonDefinitions.Records.MatchRecord();
                model.MatchRecord.Id = matchid;
                model.MatchRecord.Started = true;
                model.MatchRecord.Id= matchid;
                matchResponse = MatchService.ListMatch(model);
                if (matchResponse.Success && matchResponse.MatchRecords.Count() > 0)
                {
                    return Ok(matchResponse);
                }
                else
                {
                    matchResponse.Success = false;
                    matchResponse.StatusCode = HttpStatusCode.OK;
                    return Ok(matchResponse);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// Receive NotPlayedMatch
        /// </summary>
        [HttpGet]
        [Route("NoPointsMatch")]
        [Produces("application/json")]
        public IActionResult NoPointsMatch()
        {
            var matchResponse = new MatchResponse();
            var jsonString = string.Empty;
            var responseResult = string.Empty;
            try
            {

                //LogHelper.LogInfo("Test Log");
                // _logger.LogInformation("Test Log");
                var model = new MatchRequest();

                model._context = _context;
                model.MatchRecord = new Fantasy_Data.CommonDefinitions.Records.MatchRecord();
                model.MatchRecord.NoPoints = true;
                model.MatchRecord.IsSync = false;
                //model.MatchRecord.Id = 760;
                matchResponse = MatchService.ListMatch(model);
                //ToDo send to nagy 
                if (matchResponse.Success && matchResponse.MatchRecords.Count > 0)
                {
                    foreach (var matchRecord in matchResponse.MatchRecords)
                    {
                        try
                        {
                            jsonString = JsonConvert.SerializeObject(matchRecord, Formatting.None);

                            string fantasyTeams = _config.GetValue<string>("Url:FantasyTeams");
                            var url = fantasyTeams + "FantasyTeams/EndMatch";
                            string userName = _config.GetValue<string>("Secret:UserName");
                            string password = _config.GetValue<string>("Secret:Password");
                            _logger.LogInformation(url);
                            _logger.LogInformation(jsonString);
                            var httpResponse =
                                UIHelper.AddRequestToServiceApi(
                                    url, jsonString, userName, password);
                            responseResult = httpResponse.Content.ReadAsStringAsync().Result;
                            _logger.LogInformation(responseResult);
                        }
                        catch (Exception ex)
                        {
                            matchResponse.Message = ex.Message;
                            matchResponse.Success = false;
                            _logger.LogInformation(ex.Message + ex.StackTrace);
                            return Ok(ex.Message + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                matchResponse.Message = ex.Message;
                matchResponse.Success = false;
                _logger.LogInformation(ex.Message + ex.StackTrace);
                return Ok(ex.Message + ex.StackTrace);
            }
            //_logger.LogInformation( _context.Database.GetDbConnection().ConnectionString);
            return Ok(/*jsonString + " " +*/ responseResult);
        }

        /// <summary>
        /// Return Match With id .
        /// </summary>
        [HttpGet("GetMatchLocalize/{Matchid}", Name = "GetMatchLocalize")]
        [Produces("application/json")]
        public IActionResult GetMatchLocalize(int Matchid)
        {
            var matchLocalizeResponse = new MatchLocalizeResponse();
            try
            {
                var matchLocalizeRequest = new MatchLocalizeRequest
                {
                    _context = _context,
                    MatchLocalizeRecord = new MatchLocalizeRecord
                    {
                        MatchId = Matchid
                    }
                };
                matchLocalizeResponse = MatchLocalizeService.ListMatchLocalize(matchLocalizeRequest);
            }
            catch (Exception ex)
            {
                matchLocalizeResponse.Message = ex.Message;
                matchLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(matchLocalizeResponse);
        }

        /// <summary>
        /// Creates MatchLocalize.
        /// </summary>
        [HttpPost]
        [Route("AddMatchLocalize")]
        [Produces("application/json")]
        public IActionResult AddMatchLocalize([FromBody] MatchLocalizeRequest model)
        {
            var matchLocalizeResponse = new MatchLocalizeResponse();
            try
            {
                if (model == null)
                {
                    matchLocalizeResponse.Message = "Empty Body";
                    matchLocalizeResponse.Success = false;
                    return Ok(matchLocalizeResponse);
                }

                var editedTranslateMatch = model.MatchLocalizeRecords.Where(c => c.Id > 0).ToList();
                if (editedTranslateMatch.Count() > 0)
                {
                    var editReq = new MatchLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        MatchLocalizeRecords = editedTranslateMatch
                    };
                    matchLocalizeResponse = MatchLocalizeService.EditMatchLocalize(editReq);
                }
     
                var addedTranslateMatch = model.MatchLocalizeRecords.Where(c => c.Id == 0).ToList();
                if (addedTranslateMatch.Count() > 0)
                {
                    var addReq = new MatchLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        MatchLocalizeRecords = addedTranslateMatch
                    };
                    matchLocalizeResponse = MatchLocalizeService.AddMatchLocalize(addReq);
                }
           
            }
            catch (Exception ex)
            {
                matchLocalizeResponse.Message = ex.Message;
                matchLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(matchLocalizeResponse);
        }




        /// <summary>
        /// Remove MatchLocalize .
        /// </summary>
        [HttpPost]
        [Route("DeleteMatchLocalize")]
        [Produces("application/json")]
        public IActionResult DeleteMatchLocalize([FromBody] MatchLocalizeRequest model)
        {
            var matchLocalizeResponse = new MatchLocalizeResponse();
            try
            {
                if (model == null)
                {
                    matchLocalizeResponse.Message = "Empty Body";
                    matchLocalizeResponse.Success = false;
                    return Ok(matchLocalizeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                matchLocalizeResponse = MatchLocalizeService.DeleteMatchLocalize(model);
            }
            catch (Exception ex)
            {
                matchLocalizeResponse.Message = ex.Message;
                matchLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(matchLocalizeResponse);
        }
    }
}
