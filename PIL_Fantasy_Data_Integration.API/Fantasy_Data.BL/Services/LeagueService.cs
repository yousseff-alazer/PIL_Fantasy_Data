using System;
using System.Linq;
using System.Net;
using PIL_Fantasy_Data_Integration.API.BL.Services.Managers;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;

namespace PIL_Fantasy_Data_Integration.API.BL.Services
{
    public class LeagueService : BaseService
    {
        public static LeagueResponse ListLeague(LeagueRequest request)
        {
            var res = new LeagueResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var query = request._context.Leagues
                        .Where(c => !c.IsDeleted.Value && c.StartDate <= DateTime.Now.AddDays(-11) &&
                                    c.EndDate.Date >= DateTime.Now.Date).Select(c => new LeagueRecord
                        {
                            Id = c.Id,
                            DefaultImageUrl = c.DefaultImageUrl,
                            EndDate = c.EndDate,
                            StartDate = c.StartDate,
                            Color = c.Color,
                            IntegrationId = c.IntegrationId,
                            LeagueCountry = c.LeagueCountry,
                            LeagueCountryCode = c.LeagueCountryCode,
                            LeagueIsFriendly = c.LeagueIsFriendly,
                            LeagueType = c.LeagueType,
                            VendorId = c.VendorId ?? 1,
                            CreationDate = c.CreationDate,
                            Show = c.Show,
                                        //Name = !string.IsNullOrWhiteSpace(request.LanguageId) &&
                                        //       c.LeagueLocalizes != null && c.LeagueLocalizes.FirstOrDefault(t =>
                                        //           t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) != null
                                        //    ? c.LeagueLocalizes.FirstOrDefault(t =>
                                        //        t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)).Name
                                        //    : c.Name
                                        LeagueLocalize = !string.IsNullOrWhiteSpace(request.LanguageId) ? c.LeagueLocalizes.Where(t =>
                     t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) : null,
                                        Name = c.Name,
                                    });
                    //var x= query.ToList();
                    //if (request.LeagueRecord != null)
                    query = LeagueServiceManager.ApplyFilter(query, request);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0
                        ? ApplyPaging(query, request.PageSize, request.PageIndex)
                        : ApplyPaging(query, request.DefaultPageSize, 0);

                    var records = query.ToList();

                    res.LeagueRecords = records;
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