using System;
using System.Linq;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Enums;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;

namespace PIL_Fantasy_Data_Integration.API.BL.Services.Managers
{
    public class MatchServiceManager
    {

        public static IQueryable<MatchRecord> ApplyFilter(IQueryable<MatchRecord> query, MatchRequest matchRequest)
        {
            if (matchRequest.MatchRecord != null)
            {
                if (matchRequest.MatchRecord.Id > 0)
                    query = query.Where(c => c.Id == matchRequest.MatchRecord.Id);

                if (matchRequest.MatchRecord.LeagueId > 0)
                    query = query.Where(c => c.LeagueId == matchRequest.MatchRecord.LeagueId);

                if (matchRequest.MatchRecord.Week > 0)
                    query = query.Where(c => c.Week == matchRequest.MatchRecord.Week);
                if (matchRequest.MatchRecord.Current!=null)
                    query = query.Where(c => c.StartDatetime>DateTime.Now&&c.StartDatetime.Date<DateTime.Now.Date.AddDays(8)&&c.EndDatetime==null&&c.Status!=null&&c.Status== "Not Started");
                if (matchRequest.MatchRecord.EndToday)
                {
                    query = query.Where(c =>  c.StartDatetime.Date >= DateTime.UtcNow.Date.AddDays(-1)&&(c.EndDatetime!=null&& c.EndDatetime.Value.Date == DateTime.UtcNow.Date));
                }
                if (matchRequest.MatchRecord.Started)
                {
                    query = query.Where(c => c.StartDatetime >= DateTime.UtcNow);
                }
                if (matchRequest.MatchRecord.IsSync != null)
                {
                    query = query.Where(c => c.IsSync != null && c.IsSync == matchRequest.MatchRecord.IsSync.Value);
                }
                if (matchRequest.MatchRecord.NotPlayed)
                {
                    query = query.Where(c => (c.StartDatetime.Date == DateTime.UtcNow.Date) &&
                     (c.Status.Contains("Cancelled") || c.Status.Contains("Postponed") || c.Status.Contains("Suspended")));
                }
                if (matchRequest.MatchRecord.NoPoints)
                {
                    query = query.Where(c => (c.EndDatetime != null &&DateTime.UtcNow>=c.EndDatetime.Value.AddMinutes(33)&& DateTime.UtcNow <= c.EndDatetime.Value.AddMinutes(99)) &&(c.PlayerMatchRatings==null||!c.PlayerMatchRatings.Any()));
                }
            }
            if (matchRequest.MatchRecord?.LeagueIds != null && matchRequest.MatchRecord.LeagueIds.Count > 0)
                query = query.Where(c => matchRequest.MatchRecord.LeagueIds.Contains(c.LeagueId));
            return query;
        }
    }
}