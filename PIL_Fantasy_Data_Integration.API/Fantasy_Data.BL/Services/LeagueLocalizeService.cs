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
    public class LeagueLocalizeService : BaseService
    {
        public static LeagueLocalizeResponse ListLeagueLocalize(LeagueLocalizeRequest request)
        {
            var res = new LeagueLocalizeResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.LeagueLocalizes.Where(c => !c.IsDeleted.Value).Select(c =>
                        new LeagueLocalizeRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Name = c.Name,
                            LanguageId = c.LanguageId,
                            LeagueId=c.LeagueId
                        });

                    if (request.LeagueLocalizeRecord != null)
                        query = LeagueLocalizeServiceManager.ApplyFilter(query, request.LeagueLocalizeRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.LeagueLocalizeRecords = query.ToList();
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

        public static LeagueLocalizeResponse DeleteLeagueLocalize(LeagueLocalizeRequest request)
        {
            var res = new LeagueLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.LeagueLocalizeRecord;
                    var leagueLocalize =
                        request._context.LeagueLocalizes.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
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

        public static LeagueLocalizeResponse EditLeagueLocalize(LeagueLocalizeRequest request)
        {
            var res = new LeagueLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.LeagueLocalizeRecords)
                    {
                        var leagueLocalize = request._context.LeagueLocalizes.Find(model.Id);
                        if (leagueLocalize != null)
                        {
                            //update whole leagueLocalize
                            leagueLocalize = LeagueLocalizeServiceManager.AddOrEditLeagueLocalize(request.BaseUrl,
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

        public static LeagueLocalizeResponse AddLeagueLocalize(LeagueLocalizeRequest request)
        {
            var res = new LeagueLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.LeagueLocalizeRecords)
                    {
                        var LeagueLocalizeExist = request._context.LeagueLocalizes.Any(m =>
                            m.Name.ToLower() == model.Name.ToLower() && !m.IsDeleted.Value && m.LanguageId == model.LanguageId && m.LeagueId == model.LeagueId);
                        if (!LeagueLocalizeExist)
                        {
                            var leagueLocalize =
                                LeagueLocalizeServiceManager.AddOrEditLeagueLocalize(request.BaseUrl,
                                    model);
                            request._context.LeagueLocalizes.Add(leagueLocalize);
                            request._context.SaveChanges();
                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "LeagueLocalize already exist";
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