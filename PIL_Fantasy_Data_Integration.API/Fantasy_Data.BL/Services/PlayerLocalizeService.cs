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
    public class PlayerLocalizeService : BaseService
    {
        public static PlayerLocalizeResponse ListPlayerLocalize(PlayerLocalizeRequest request)
        {
            var res = new PlayerLocalizeResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.PlayerLocalizes.Where(c => !c.IsDeleted.Value).Select(c =>
                        new PlayerLocalizeRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Name = c.Name,
                            LanguageId = c.LanguageId,
                            PlayerId=c.PlayerId
                        });

                    if (request.PlayerLocalizeRecord != null)
                        query = PlayerLocalizeServiceManager.ApplyFilter(query, request.PlayerLocalizeRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.PlayerLocalizeRecords = query.ToList();
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

        public static PlayerLocalizeResponse DeletePlayerLocalize(PlayerLocalizeRequest request)
        {
            var res = new PlayerLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.PlayerLocalizeRecord;
                    var playerLocalize =
                        request._context.PlayerLocalizes.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (playerLocalize != null)
                    {
                        //update playerLocalize IsDeleted
                        playerLocalize.IsDeleted = true;
                        playerLocalize.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid playerLocalize";
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

        public static PlayerLocalizeResponse EditPlayerLocalize(PlayerLocalizeRequest request)
        {
            var res = new PlayerLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.PlayerLocalizeRecords)
                    {
                        var playerLocalize = request._context.PlayerLocalizes.Find(model.Id);
                        if (playerLocalize != null)
                        {
                            //update whole playerLocalize
                            playerLocalize = PlayerLocalizeServiceManager.AddOrEditPlayerLocalize(request.BaseUrl,
                                model, playerLocalize);
                            request._context.SaveChanges();

                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "Invalid playerLocalize";
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

        public static PlayerLocalizeResponse AddPlayerLocalize(PlayerLocalizeRequest request)
        {
            var res = new PlayerLocalizeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.PlayerLocalizeRecords)
                    {
                        var PlayerLocalizeExist = request._context.PlayerLocalizes.Any(m =>
                            m.Name.ToLower() == model.Name.ToLower() && !m.IsDeleted.Value && m.LanguageId == model.LanguageId && m.PlayerId == model.PlayerId);
                        if (!PlayerLocalizeExist)
                        {
                            var playerLocalize =
                                PlayerLocalizeServiceManager.AddOrEditPlayerLocalize(request.BaseUrl,
                                    model);
                            request._context.PlayerLocalizes.Add(playerLocalize);
                            request._context.SaveChanges();
                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "PlayerLocalize already exist";
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