using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using Microsoft.AspNetCore.Http;
using System.IO;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;

namespace PIL_Fantasy_Data_Integration.API.BL.Services.Managers
{
    public class TeamServiceManager
    {
        public static IQueryable<TeamRecord> ApplyFilter(IQueryable<TeamRecord> query, TeamRequest teamRequest)
        {
            if (teamRequest.TeamRecord != null)
            {
                if (teamRequest.TeamRecord.Id > 0)
                    query = query.Where(c => c.Id == teamRequest.TeamRecord.Id);
            }
            if (!string.IsNullOrWhiteSpace(teamRequest.Name))
            {
                var filterName = teamRequest.Name.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(filterName));
            }
            //if (teamRequest.TeamRecord != null && teamRequest.TeamRecord.TeamPerformApiId.Count > 0)
            //    query = query.Where(c => teamRequest.TeamRecord.TeamPerformApiId.Contains(c.PerformApiId));

            return query;
        }
    }
}