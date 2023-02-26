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
    public class FantasyRuleLocalizeService : BaseService
    {
        public static FantasyRuleLocalizeResponse ListFantasyRuleLocalize(FantasyRuleLocalizeRequest request)
        {
            var res = new FantasyRuleLocalizeResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.FantasyRuleLocalizes.Where(c => !c.IsDeleted.Value).Select(c =>
                        new FantasyRuleLocalizeRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Description = c.Description,
                            Title = c.Title,
                            LanguageId = c.LanguageId,
                            FantasyRuleId = c.FantasyRuleId
                        });

                    if (request.FantasyRuleLocalizeRecord != null)
                        query = FantasyRuleLocalizeServiceManager.ApplyFilter(query, request.FantasyRuleLocalizeRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.FantasyRuleLocalizeRecords = query.ToList();
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

        public static FantasyRuleLocalizeResponse DeleteFantasyRuleLocalize(FantasyRuleLocalizeRequest request)
        {
            var res = new FantasyRuleLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.FantasyRuleLocalizeRecord;
                    var leagueLocalize =
                        request._context.FantasyRuleLocalizes.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
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

        public static FantasyRuleLocalizeResponse EditFantasyRuleLocalize(FantasyRuleLocalizeRequest request)
        {
            var res = new FantasyRuleLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.FantasyRuleLocalizeRecords)
                    {
                        var leagueLocalize = request._context.FantasyRuleLocalizes.Find(model.Id);
                        if (leagueLocalize != null)
                        {
                            //update whole leagueLocalize
                            leagueLocalize = FantasyRuleLocalizeServiceManager.AddOrEditFantasyRuleLocalize(request.BaseUrl,
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

        public static FantasyRuleLocalizeResponse AddFantasyRuleLocalize(FantasyRuleLocalizeRequest request)
        {
            var res = new FantasyRuleLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.FantasyRuleLocalizeRecords)
                    {
                        var FantasyRuleLocalizeExist = request._context.FantasyRuleLocalizes.Any(m =>
                            m.Title.ToLower() == model.Title.ToLower() && !m.IsDeleted.Value && m.LanguageId == model.LanguageId && m.FantasyRuleId == model.FantasyRuleId);
                        if (!FantasyRuleLocalizeExist)
                        {
                            var leagueLocalize =
                                FantasyRuleLocalizeServiceManager.AddOrEditFantasyRuleLocalize(request.BaseUrl,
                                    model);
                            request._context.FantasyRuleLocalizes.Add(leagueLocalize);
                            request._context.SaveChanges();
                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "FantasyRuleLocalize already exist";
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