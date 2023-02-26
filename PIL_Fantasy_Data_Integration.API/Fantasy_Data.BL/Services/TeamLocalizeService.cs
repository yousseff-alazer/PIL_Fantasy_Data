using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions;
using System.Net;
using PIL_Fantasy_Data_Integration.API.BL.Services.Managers;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;

namespace PIL_Fantasy_Data_Integration.API.BL.Services
{
    public class TeamLocalizeService : BaseService
    {
        public static TeamLocalizeResponse ListTeamLocalize(TeamLocalizeRequest request)
        {
            var res = new TeamLocalizeResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.TeamLocalizes.Where(c => !c.IsDeleted.Value).Select(c =>
                        new TeamLocalizeRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Name = c.Name,
                            LanguageId = c.LanguageId,
                            TeamId = c.TeamId
                        });

                    if (request.TeamLocalizeRecord != null)
                        query = TeamLocalizeServiceManager.ApplyFilter(query, request.TeamLocalizeRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.TeamLocalizeRecords = query.ToList();
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

        public static TeamLocalizeResponse DeleteTeamLocalize(TeamLocalizeRequest request)
        {
            var res = new TeamLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TeamLocalizeRecord;
                    var leagueLocalize =
                        request._context.TeamLocalizes.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (leagueLocalize != null)
                    {
                        //update leagueLocalize IsDeleted
                        leagueLocalize.IsDeleted = true;
                        leagueLocalize.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid leagueLocalize";
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

        public static TeamLocalizeResponse EditTeamLocalize(TeamLocalizeRequest request)
        {
            var res = new TeamLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.TeamLocalizeRecords)
                    {
                        var leagueLocalize = request._context.TeamLocalizes.Find(model.Id);
                        if (leagueLocalize != null)
                        {
                            //update whole leagueLocalize
                            leagueLocalize = TeamLocalizeServiceManager.AddOrEditTeamLocalize(request.BaseUrl,
                                model, leagueLocalize);
                            request._context.SaveChanges();

                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "Invalid leagueLocalize";
                            res.Success = false;
                        }
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

        public static TeamLocalizeResponse AddTeamLocalize(TeamLocalizeRequest request)
        {
            var res = new TeamLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.TeamLocalizeRecords)
                    {
                        var TeamLocalizeExist = request._context.TeamLocalizes.Any(m =>
                            m.Name.ToLower() == model.Name.ToLower() && !m.IsDeleted.Value && m.LanguageId == model.LanguageId && m.TeamId == model.TeamId);
                        if (!TeamLocalizeExist)
                        {
                            var leagueLocalize =
                                TeamLocalizeServiceManager.AddOrEditTeamLocalize(request.BaseUrl,
                                    model);
                            request._context.TeamLocalizes.Add(leagueLocalize);
                            request._context.SaveChanges();
                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "TeamLocalize already exist";
                            res.Success = false;
                        }
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