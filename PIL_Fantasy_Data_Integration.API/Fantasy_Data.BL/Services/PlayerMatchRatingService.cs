using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using System.Net;
using PIL_Fantasy_Data_Integration.API.BL.Services.Managers;

namespace PIL_Fantasy_Data_Integration.API.BL.Services
{
    public class PlayerMatchRatingService : BaseService
    {
        public static PlayerMatchRatingResponse ListPlayerMatchRating(PlayerMatchRatingRequest request)
        {
            var res = new PlayerMatchRatingResponse();
            RunBase(request, res, (PlayerMatchRatingRequest req) =>
             {
                 try
                 {
                     var query = request._context.PlayerMatchRatings.Where(c => !c.IsDeleted.Value).Select(c => new PlayerMatchRatingRecord
                     {
                         Id = c.Id,
                         CreationDate = c.CreationDate,
                         TeamId = c.TeamId,
                         Minutes = c.Minutes,
                         Rating = c.Rating,
                         MatchId = c.MatchId,
                         PlayerId = c.PlayerId
                     });

                     if (request != null)
                         query = PlayerMatchRatingServiceManager.ApplyFilter(query, request);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);
                     //request.PageSize = 5;
                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                     res.PlayerMatchRatingRecords = query.ToList();
                     res.Message = HttpStatusCode.OK.ToString();
                     res.Success = true;
                     res.StatusCode = HttpStatusCode.OK;
                 }
                 catch (Exception ex)
                 {
                     res.Message = ex.Message;
                     res.Success = false;
                     LogHelper.LogException(ex.Message, ex.StackTrace);
                 }
                 return res;
             });
            return res;
        }

    }
}