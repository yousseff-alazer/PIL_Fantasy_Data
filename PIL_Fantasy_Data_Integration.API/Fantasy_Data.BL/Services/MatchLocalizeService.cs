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
    public class MatchLocalizeService : BaseService
    {
        public static MatchLocalizeResponse ListMatchLocalize(MatchLocalizeRequest request)
        {
            var res = new MatchLocalizeResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.MatchLocalizes.Where(c => !c.IsDeleted.Value).Select(c =>
                        new MatchLocalizeRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Description = c.Description,
                            Title = c.Title,
                            LanguageId = c.LanguageId,
                            MatchId = c.MatchId
                        });

                    if (request.MatchLocalizeRecord != null)
                        query = MatchLocalizeServiceManager.ApplyFilter(query, request.MatchLocalizeRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.MatchLocalizeRecords = query.ToList();
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

        public static MatchLocalizeResponse DeleteMatchLocalize(MatchLocalizeRequest request)
        {
            var res = new MatchLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.MatchLocalizeRecord;
                    var leagueLocalize =
                        request._context.MatchLocalizes.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
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

        public static MatchLocalizeResponse EditMatchLocalize(MatchLocalizeRequest request)
        {
            var res = new MatchLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.MatchLocalizeRecords)
                    {
                        var leagueLocalize = request._context.MatchLocalizes.Find(model.Id);
                        if (leagueLocalize != null)
                        {
                            //update whole leagueLocalize
                            leagueLocalize = MatchLocalizeServiceManager.AddOrEditMatchLocalize(request.BaseUrl,
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

        public static MatchLocalizeResponse AddMatchLocalize(MatchLocalizeRequest request)
        {
            var res = new MatchLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.MatchLocalizeRecords)
                    {
                        var MatchLocalizeExist = request._context.MatchLocalizes.Any(m =>
                            m.Title.ToLower() == model.Title.ToLower() && !m.IsDeleted.Value && m.LanguageId == model.LanguageId && m.MatchId == model.MatchId);
                        if (!MatchLocalizeExist)
                        {
                            var leagueLocalize =
                                MatchLocalizeServiceManager.AddOrEditMatchLocalize(request.BaseUrl,
                                    model);
                            request._context.MatchLocalizes.Add(leagueLocalize);
                            request._context.SaveChanges();
                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "MatchLocalize already exist";
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