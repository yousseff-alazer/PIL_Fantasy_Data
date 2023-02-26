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
    public class TeamService : BaseService
    {
        public static TeamResponse ListTeam(TeamRequest request)
        {
            var res = new TeamResponse();
            RunBase(request, res, (TeamRequest req) =>
             {
                 try
                 {
                     var query = request._context.Teams.Where(c => !c.IsDeleted.Value).Select(c => new TeamRecord
                     {
                         Id = c.Id,
                         CreationDate = c.CreationDate,
                         LeagueId = c.LeagueId,
                         ImageUrl = c.ImageUrl,
                         OrderInLeague = c.OrderInLeague,
                         WonCount = c.WonCount,
                         LossCount = c.LossCount,
                         DrawCount = c.DrawCount,
                         Group = c.Group,
                         Points = c.Points,
                         PlayedCount = c.PlayedCount,
                         GoalsAgainst = c.GoalsAgainst,
                         GoalsFor = c.GoalsFor,
                         //Name = !string.IsNullOrWhiteSpace(request.LanguageId) &&
                         //       c.TeamLocalizes != null && c.TeamLocalizes.FirstOrDefault(t =>
                         //           t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) != null
                         //    ? c.TeamLocalizes.FirstOrDefault(t =>
                         //        t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)).Name
                         //    : c.Name
                         TeamLocalize = !string.IsNullOrWhiteSpace(request.LanguageId) ? c.TeamLocalizes.Where(t =>
t.LanguageId == request.LanguageId && !string.IsNullOrWhiteSpace(t.Name)) : null,
                         Name = c.Name,
                     });

                     //if (request.TeamRecord != null)
                     query = TeamServiceManager.ApplyFilter(query, request);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                     res.TeamRecords = query.ToList();
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