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
    public class FantasyRuleService : BaseService
    {
        public static FantasyRuleResponse ListFantasyRule(FantasyRuleRequest request)
        {
            var res = new FantasyRuleResponse();
            RunBase(request, res, (FantasyRuleRequest req) =>
             {

                 try
                 {
                     var query = request._context.FantasyRules.Where(c => !c.IsDeleted.Value).Select(c => new FantasyRuleRecord
                     {
                         Id = c.Id,
                         Description = c.Description,
                         Message = c.Message,
                         Title=c.Title,
                         Max = c.Max,
                         Min = c.Min
                     });

                     if (request.FantasyRuleRecord != null)
                         query = FantasyRuleServiceManager.ApplyFilter(query, request.FantasyRuleRecord);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                     res.FantasyRuleRecords = query.ToList();
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
        public static FantasyRuleResponse DeleteFantasyRule(FantasyRuleRequest request)
        {
            var res = new FantasyRuleResponse();
            RunBase(request, res, (FantasyRuleRequest req) =>
            {
                try
                {
                    var model = request.FantasyRuleRecord;
                    var fantasyRule = request._context.FantasyRules.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (fantasyRule != null)
                    {
                        //update fantasyRule IsDeleted
                        fantasyRule.IsDeleted = true;
                        fantasyRule.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid fantasyRule";
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

        public static FantasyRuleResponse EditFantasyRule(FantasyRuleRequest request)
        {
            var res = new FantasyRuleResponse();
            RunBase(request, res, (FantasyRuleRequest req) =>
            {
                try
                {
                    var model = request.FantasyRuleRecord;
                    var fantasyRule = request._context.FantasyRules.Find(model.Id);
                    if (fantasyRule != null)
                    {
                        //update whole fantasyRule
                        fantasyRule = FantasyRuleServiceManager.AddOrEditFantasyRule(request.FantasyRuleRecord.CreatedBy, request.FantasyRuleRecord, fantasyRule);
                        request._context.SaveChanges();
                        if (request.FantasyRuleRecord.PostitionId != null && request.FantasyRuleRecord.PostitionId > 0)
                        {
                            var oldPosRule = request._context.PositionRules.FirstOrDefault(c=>c.PositionId== request.FantasyRuleRecord.PostitionId.Value);
                            if (oldPosRule!=null)
                            {
                                oldPosRule.IsDeleted = true;
                            }
                            var positionRule = new PositionRule
                            {
                                PositionId = request.FantasyRuleRecord.PostitionId.Value,
                                RuleId = fantasyRule.Id
                            };
                            request._context.Add(positionRule);
                            request._context.SaveChanges();
                        }
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid fantasyRule";
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

        public static FantasyRuleResponse AddFantasyRule(FantasyRuleRequest request)
        {
            var res = new FantasyRuleResponse();
            RunBase(request, res, (FantasyRuleRequest req) =>
            {
                try
                {
                    var FantasyRuleExist = request._context.FantasyRules.Any(c => !c.IsDeleted.Value && c.Title.IndexOf(request.FantasyRuleRecord.Title,
                                                                           StringComparison.OrdinalIgnoreCase) >= 0);
                    //var FantasyRuleExist = request._context.FantasyRuleLocalize.Any(c => !c.IsDeleted.Value && !c.FantasyRule.IsDeleted.Value && request.FantasyRuleRecord.FantasyRuleLocalizeList.Any(l => l.Name.ToLower() == c.Name.ToLower() && l.LanguageId == c.LanguageId));
                    if (!FantasyRuleExist)
                    {
                        var fantasyRule = FantasyRuleServiceManager.AddOrEditFantasyRule(request.FantasyRuleRecord.CreatedBy, request.FantasyRuleRecord);
                        request._context.FantasyRules.Add(fantasyRule);

                        request._context.SaveChanges();
                        if (request.FantasyRuleRecord.PostitionId!=null&&request.FantasyRuleRecord.PostitionId>0)
                        {
                            var positionRule = new PositionRule
                            { 
                                PositionId = request.FantasyRuleRecord.PostitionId.Value,
                                RuleId = fantasyRule.Id
                            };
                            request._context.Add(positionRule);
                            request._context.SaveChanges();
                        }
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "FantasyRule already exist";
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
