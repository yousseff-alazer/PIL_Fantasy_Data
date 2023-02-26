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
    public class FantasyRulesController : ControllerBase
    {
        private readonly fantasy_dataContext _context;
        private readonly ILogger<FantasyRulesController> _logger;
        private readonly IConfiguration _config;
        //private readonly IDistributedCache _cache;
        public FantasyRulesController(fantasy_dataContext context, ILogger<FantasyRulesController> logger, IConfiguration config/*, IDistributedCache cache*/)
        {
            _config = config;
            _logger = logger;
            _context = context;
            //_cache = cache;
        }

        /// <summary>
        /// Return List Of FantasyRule With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] FantasyRuleRequest model)
        {
            var fantasyRuleResponse = new FantasyRuleResponse();
            try
            {
                model ??= new FantasyRuleRequest();
                model._context = _context;
                // Get data from cache
                //var cacheKey = $"fantasyRules_{DateTime.UtcNow.Date.ToShortDateString()}";
                //if (model.FantasyRuleRecord != null &&
                //    model.FantasyRuleRecord.Current != null
                //    && model.FantasyRuleRecord.Current.Value)
                //{
                //    if (model.FantasyRuleRecord.LeagueId > 0)
                //    {
                //        cacheKey = $"fantasyRules_{model.FantasyRuleRecord.LeagueId}_{DateTime.UtcNow.Date.ToShortDateString()}";
                //    }
                //    else
                //    {
                //        cacheKey = $"fantasyRules_current_{DateTime.UtcNow.Date.ToShortDateString()}";
                //    }
                //}
                //var cachedData = _cache.Get(cacheKey);
                //if (cachedData != null)
                //{
                //    // If data found in cache, encode and deserialize cached data
                //    var cachedDataString = Encoding.UTF8.GetString(cachedData);
                //    fantasyRuleResponse = JsonConvert.DeserializeObject<FantasyRuleResponse>(cachedDataString);
                //}
                //else
                //{
                    //If not found, then fetch data from database
                    fantasyRuleResponse = FantasyRuleService.ListFantasyRule(model);
                    // serialize data
                    //var cachedDataString = JsonConvert.SerializeObject(fantasyRuleResponse);
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
                fantasyRuleResponse.Message = ex.Message;
                fantasyRuleResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(fantasyRuleResponse);
        }

        
        /// <summary>
        /// Return FantasyRule With id .
        /// </summary>
        [HttpGet("GetFantasyRuleLocalize/{FantasyRuleid}", Name = "GetFantasyRuleLocalize")]
        [Produces("application/json")]
        public IActionResult GetFantasyRuleLocalize(int FantasyRuleid)
        {
            var fantasyRuleLocalizeResponse = new FantasyRuleLocalizeResponse();
            try
            {
                var fantasyRuleLocalizeRequest = new FantasyRuleLocalizeRequest
                {
                    _context = _context,
                    FantasyRuleLocalizeRecord = new FantasyRuleLocalizeRecord
                    {
                        FantasyRuleId = FantasyRuleid
                    }
                };
                fantasyRuleLocalizeResponse = FantasyRuleLocalizeService.ListFantasyRuleLocalize(fantasyRuleLocalizeRequest);
            }
            catch (Exception ex)
            {
                fantasyRuleLocalizeResponse.Message = ex.Message;
                fantasyRuleLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(fantasyRuleLocalizeResponse);
        }

        /// <summary>
        /// Creates FantasyRuleLocalize.
        /// </summary>
        [HttpPost]
        [Route("AddFantasyRuleLocalize")]
        [Produces("application/json")]
        public IActionResult AddFantasyRuleLocalize([FromBody] FantasyRuleLocalizeRequest model)
        {
            var fantasyRuleLocalizeResponse = new FantasyRuleLocalizeResponse();
            try
            {
                if (model == null)
                {
                    fantasyRuleLocalizeResponse.Message = "Empty Body";
                    fantasyRuleLocalizeResponse.Success = false;
                    return Ok(fantasyRuleLocalizeResponse);
                }

                var editedTranslateFantasyRule = model.FantasyRuleLocalizeRecords.Where(c => c.Id > 0).ToList();
                if (editedTranslateFantasyRule.Count()>0)
                {
                    var editReq = new FantasyRuleLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        FantasyRuleLocalizeRecords = editedTranslateFantasyRule
                    };
                    fantasyRuleLocalizeResponse = FantasyRuleLocalizeService.EditFantasyRuleLocalize(editReq);
                }

                var addedTranslateFantasyRule = model.FantasyRuleLocalizeRecords.Where(c => c.Id == 0).ToList();
                if (addedTranslateFantasyRule.Count()>0)
                {
                    var addReq = new FantasyRuleLocalizeRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        FantasyRuleLocalizeRecords = addedTranslateFantasyRule
                    };
                    fantasyRuleLocalizeResponse = FantasyRuleLocalizeService.AddFantasyRuleLocalize(addReq);
                }
              
            }
            catch (Exception ex)
            {
                fantasyRuleLocalizeResponse.Message = ex.Message;
                fantasyRuleLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(fantasyRuleLocalizeResponse);
        }




        /// <summary>
        /// Remove FantasyRuleLocalize .
        /// </summary>
        [HttpPost]
        [Route("DeleteFantasyRuleLocalize")]
        [Produces("application/json")]
        public IActionResult DeleteFantasyRuleLocalize([FromBody] FantasyRuleLocalizeRequest model)
        {
            var fantasyRuleLocalizeResponse = new FantasyRuleLocalizeResponse();
            try
            {
                if (model == null)
                {
                    fantasyRuleLocalizeResponse.Message = "Empty Body";
                    fantasyRuleLocalizeResponse.Success = false;
                    return Ok(fantasyRuleLocalizeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                fantasyRuleLocalizeResponse = FantasyRuleLocalizeService.DeleteFantasyRuleLocalize(model);
            }
            catch (Exception ex)
            {
                fantasyRuleLocalizeResponse.Message = ex.Message;
                fantasyRuleLocalizeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(fantasyRuleLocalizeResponse);
        }
    }
}
