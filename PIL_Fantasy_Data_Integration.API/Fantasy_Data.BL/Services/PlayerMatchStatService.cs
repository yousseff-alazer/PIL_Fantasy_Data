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
    public class PlayerMatchStatService : BaseService
    {
        public static PlayerMatchStatResponse ListPlayerMatchStat(PlayerMatchStatRequest request)
        {
            var res = new PlayerMatchStatResponse();
            RunBase(request, res, (PlayerMatchStatRequest req) =>
             {
                 try
                 {
                     var query = request._context.PlayerMatchStats.Where(c => !c.IsDeleted.Value).Select(c => new PlayerMatchStatRecord
                     {
                         Id = c.Id,
                         CreationDate = c.CreationDate,
                         TeamId = c.TeamId,
                         CardsRed = c.CardsRed,
                         CardsYellow = c.CardsYellow,
                         GoalsAssists = c.GoalsAssists,
                         GoalsTotal = c.GoalsTotal,
                         PassesAccuracy = c.PassesAccuracy,
                         PassesTotal = c.PassesTotal,
                         DribblesAttempts = c.DribblesAttempts, 
                         DribblesPast = c.DribblesPast,
                         DribblesSuccess = c.DribblesSuccess,
                         DuelsTotal = c.DuelsTotal,
                         DuelsWon = c.DuelsWon,
                         FoulsCommitted = c.FoulsCommitted,
                         FoulsDrawn = c.FoulsDrawn,
                         GoalSaves = c.GoalSaves,
                         GoalsConceded = c.GoalsConceded,
                         MatchId = c.MatchId,
                         Offsides = c.Offsides,
                         PassesKey = c.PassesKey,
                         PenaltyCommitted = c.PenaltyCommitted,
                         PenaltyMissed = c.PenaltyMissed,
                         PenaltySaved = c.PenaltySaved,
                         PenaltyScored = c.PenaltyScored,
                         PenaltyWon = c.PenaltyWon,
                         PlayerId = c.PlayerId,
                         ShotsOn = c.ShotsOn,
                         ShotsTotal = c.ShotsTotal,
                         TacklesBlocks = c.TacklesBlocks,
                         TacklesInterceptions = c.TacklesInterceptions,
                         TacklesTotal = c.TacklesTotal
                     });

                     if (request != null)
                         query = PlayerMatchStatServiceManager.ApplyFilter(query, request);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);
                     //request.PageSize = 5;
                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                     res.PlayerMatchStatRecords = query.ToList();
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