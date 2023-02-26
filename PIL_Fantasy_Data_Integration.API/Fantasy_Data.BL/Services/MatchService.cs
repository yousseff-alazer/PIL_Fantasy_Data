using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using PIL_Fantasy_Data_Integration.API.BL.Services.Managers;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;

namespace PIL_Fantasy_Data_Integration.API.BL.Services
{
    public class MatchService : BaseService
    {
        public static MatchResponse ListMatch(MatchRequest request)
        {
            var res = new MatchResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var query = request._context.Matches.Where(c => !c.IsDeleted.Value).Select(c => new MatchRecord
                    {
                        Id = c.Id,
                        IntegrationId = c.IntegrationId,
                        Team1Id = c.Team1Id,
                        Team2Id = c.Team2Id,
                        StartDatetime = c.StartDatetime,
                        EndDatetime = c.EndDatetime,
                        ModifiedBy = c.ModifiedBy,
                        Status = c.Status,
                        Team1Score = c.Team1Score,
                        Team2Score = c.Team2Score,
                        LeagueId = c.LeagueId,
                        Team1ImageUrl = c.Team1.ImageUrl,
                        Team2ImageUrl = c.Team2.ImageUrl,
                        Week = c.Week,
                        HomeTeamId = c.HomeTeamId,
                        CreationDate = c.CreationDate,
                        Team1IntegrationId = c.Team1 != null ? c.Team1.IntegrationId : "",
                        Team2IntegrationId = c.Team2 != null ? c.Team2.IntegrationId : "",
                        LeagueIntegrationId = c.League != null ? c.League.IntegrationId : "",
                        LeagueDisplayOrder = c.League != null && c.League.LeagueDisplayOrder != null ? c.League.LeagueDisplayOrder.Value : 0,
                        //Title = c.Title,
                        //Description = c.Description,
                        LeagueImageUrl = c.League != null ? c.League.DefaultImageUrl : "",
                        LeagueName = c.League != null  ? c.League.Name : "",
                        IsSync=c.IsSync,
                        PlayerMatchRatings = /*request.MatchRecord.EndToday&&*/ c.PlayerMatchRatings!=null? c.PlayerMatchRatings : null,
                        Name = c.Team1 != null && c.Team2 != null ? c.Team1.Name + " vs " + c.Team2.Name : "",
                        Team1Localize = c.Team1 != null &&
                                    !string.IsNullOrWhiteSpace(request.LanguageId)  ? c.Team1.TeamLocalizes.Where(t =>
                                t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) : null,
                        Team1Name = c.Team1 != null? c.Team1.Name :"",
                        Team2Localize = c.Team2 != null &&
                                    !string.IsNullOrWhiteSpace(request.LanguageId) ? c.Team2.TeamLocalizes.Where(t =>
                               t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) : null,
                        Team2Name = c.Team2 != null ? c.Team2.Name : "",
                        MatchLocalize= !string.IsNullOrWhiteSpace(request.LanguageId) &&
                                c.MatchLocalizes != null ? c.MatchLocalizes.Where(t =>
                                t.LanguageId == request.LanguageId) : null,
                        NoPoints= req.MatchRecord!=null && req.MatchRecord.NoPoints,
                        NotPlayed= req.MatchRecord != null && req.MatchRecord.NotPlayed
                    });

                    //if (request.MatchRecord != null)
                    query = MatchServiceManager.ApplyFilter(query, request);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    //request.PageSize = 4;
                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    var records = query.ToList();
                    res.MatchRecords = records;
                    if(!string.IsNullOrWhiteSpace(request.LanguageId)&&request.LanguageId== "ar")
                    {
                        res.Message = "اوك";
                    }
                    else
                    {
                        res.Message = HttpStatusCode.OK.ToString();
                    }
                    
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