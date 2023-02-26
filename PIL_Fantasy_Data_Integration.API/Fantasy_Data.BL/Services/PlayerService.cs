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
    public class PlayerService : BaseService
    {
        public static PlayerResponse ListPlayer(PlayerRequest request)
        {
            var res = new PlayerResponse();
            RunBase(request, res, (PlayerRequest req) =>
             {
                 try
                 {
                     var query = request._context.Players.Where(c => !c.IsDeleted.Value).Select(c => new PlayerRecord
                     {
                         Id = c.Id,
                         CreationDate = c.CreationDate,
                         TeamId = c.TeamId,
                         TeamLocalize= c.Team != null &&
                             !string.IsNullOrWhiteSpace(request.LanguageId)? c.Team.TeamLocalizes.Where(t =>
                          t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) : null,
                         TeamName= c.Team != null?c.Team.Name:"",
                         //TeamName=c.Team!=null&&
                         //    !string.IsNullOrWhiteSpace(request.LanguageId) &&
                         //c.Team.TeamLocalizes != null && c.Team.TeamLocalizes.FirstOrDefault(t =>
                         //t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) != null
                         //? c.Team.TeamLocalizes.FirstOrDefault(t =>
                         //t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)).Name
                         //: c.Team.Name,
                         PlayerLocalize = !string.IsNullOrWhiteSpace(request.LanguageId) ? c.PlayerLocalizes.Where(t =>
                                  t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) : null,
                         Name = c.Name,
                         //Name = !string.IsNullOrWhiteSpace(request.LanguageId) &&
                         //       c.PlayerLocalizes != null && c.PlayerLocalizes.FirstOrDefault(t =>
                         //           t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) != null
                         //    ? c.PlayerLocalizes.FirstOrDefault(t =>
                         //        t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)).Name
                         //    : c.Name,
                         Age = c.Age,
                         CardsRed = c.CardsRed,
                         CardsYellow = c.CardsYellow,
                         CardsYellowRed = c.CardsYellowRed,
                         CountryOfBirth = c.CountryOfBirth,
                         DateOfBirth = c.DateOfBirth,
                         GoalsAssists = c.GoalsAssists,
                         GoalsSaves = c.GoalsSaves,
                         GoalsTotal = c.GoalsTotal,
                         Height = c.Height,
                         Weight = c.Weight,
                         Nationality = c.Nationality,
                         PassesAccuracy = c.PassesAccuracy,
                         PassesTotal = c.PassesTotal,
                         Photo = c.Photo,
                         Position = c.Position,
                         Injured = c.Injured,
                         Minutes = c.Minutes,
                         Rating = c.Rating,
                         Price = c.Price,
                         Credit =!string.IsNullOrWhiteSpace(c.Rating)? (float.Parse(c.Rating)*1.5).ToString():"5",
                         PositionId = c.PositionId,
                         PositionCode = c.PositionNavigation != null ? c.PositionNavigation.Code : ""
                     });

                     if (request != null)
                         query = PlayerServiceManager.ApplyFilter(query, request);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);
                     //request.PageSize = 5;
                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);
                     HandlePlayerResponse(request, res, query);

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

        private static void HandlePlayerResponse(PlayerRequest request, PlayerResponse res, IQueryable<PlayerRecord> query)
        {
            if (request.PlayerRecord != null && request.PlayerRecord.PositionFilter != null
            && request.PlayerRecord.PositionFilter.Value == true)
            {
                var playersGroupedByPosition = query.ToList().GroupBy(c => c.PositionCode).ToList();
                var rules = request._context.FantasyRules.Where(c => !c.IsDeleted.Value).ToList();
                if (!string.IsNullOrWhiteSpace(request.LanguageId))
                {
                    foreach (var rule in rules)
                    {
                       var locTitle= rule.FantasyRuleLocalizes.FirstOrDefault(t=>!t.IsDeleted.Value&& t.LanguageId!=null&&
                                 t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Title))?.Title;
                        var locDescription = rule.FantasyRuleLocalizes.FirstOrDefault(t => !t.IsDeleted.Value && t.LanguageId != null &&
               t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Description))?.Description;
                        rule.Title =!string.IsNullOrWhiteSpace(locTitle)? locTitle : rule.Title;
                        rule.Description = !string.IsNullOrWhiteSpace(locDescription) ? locDescription : rule.Description;
                    }
                }
                var positions = request._context.PlayerPositions.Where(c => !c.IsDeleted.Value).ToList();
                var positionsRules = request._context.PositionRules.Where(c => !c.IsDeleted.Value).ToList();
                res.PlayerResRecords = new PlayerRes();
                foreach (var players in playersGroupedByPosition)
                {
                    if (players.Key == "GK")
                    {
                        res.PlayerResRecords.GK = players.ToList();
                        var posId = positions.Where(c => c.Code == "GK").FirstOrDefault()?.Id;
                        if (posId != null)
                        {
                            var positionRuleIds = positionsRules.Where(c => c.PositionId == posId).Select(c => c.RuleId);
                            if (positionRuleIds != null && positionRuleIds.Count() > 0)
                            {
                                res.PlayerResRecords.GK_Rule = rules.Where(c => positionRuleIds.Contains(c.Id)).ToList();
                                rules = rules.Except(res.PlayerResRecords.GK_Rule).ToList();
                            }
                        }
                    }
                    else if (players.Key == "DF")
                    {
                        res.PlayerResRecords.DF = players.ToList();
                        var posId = positions.Where(c => c.Code == "DF").FirstOrDefault()?.Id;
                        if (posId != null)
                        {
                            var positionRuleIds = positionsRules.Where(c => c.PositionId == posId).Select(c => c.RuleId);
                            if (positionRuleIds != null && positionRuleIds.Count() > 0)
                            {
                                res.PlayerResRecords.DF_Rule = rules.Where(c => positionRuleIds.Contains(c.Id)).ToList();
                                rules = rules.Except(res.PlayerResRecords.DF_Rule).ToList();
                            }
                        }
                    }
                    else if (players.Key == "MF")
                    {
                        res.PlayerResRecords.MF = players.ToList();
                        var posId = positions.Where(c => c.Code == "MF").FirstOrDefault()?.Id;
                        if (posId != null)
                        {
                            var positionRuleIds = positionsRules.Where(c => c.PositionId == posId).Select(c => c.RuleId);
                            if (positionRuleIds != null && positionRuleIds.Count() > 0)
                            {
                                res.PlayerResRecords.MF_Rule = rules.Where(c => positionRuleIds.Contains(c.Id)).ToList();
                                rules = rules.Except(res.PlayerResRecords.MF_Rule).ToList();
                            }
                        }
                    }
                    else if (players.Key == "FW")
                    {
                        res.PlayerResRecords.FW = players.ToList();
                        var posId = positions.Where(c => c.Code == "FW").FirstOrDefault()?.Id;
                        if (posId != null)
                        {
                            var positionRuleIds = positionsRules.Where(c => c.PositionId == posId).Select(c => c.RuleId);
                            if (positionRuleIds != null && positionRuleIds.Count() > 0)
                            {
                                res.PlayerResRecords.FW_Rule = rules.Where(c => positionRuleIds.Contains(c.Id)).ToList();
                                rules = rules.Except(res.PlayerResRecords.FW_Rule).ToList();
                            }
                        }
                    }
                }
                res.PlayerResRecords.Rules = rules;
            }
            else
            {
                res.PlayerResRecords=new PlayerRes();
                res.PlayerResRecords.PlayerRecords = query.ToList();
            }
        }

        public static PlayerResponse EditPlayer(PlayerRequest request)
        {
            var res = new PlayerResponse();
            RunBase(request, res, (PlayerRequest req) =>
            {
                try
                {
                    var model = request.PlayerRecord;
                    var player = request._context.Players.Find(model.Id);
                    if (player != null)
                    {

                        //update whole player
                        player = PlayerServiceManager.AddOrEditPlayer(request.PlayerRecord.CreatedBy, request.PlayerRecord, player);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid player";
                        res.Success = false;
                    }
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