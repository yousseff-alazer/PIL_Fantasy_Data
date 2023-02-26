using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;

namespace PIL_Fantasy_Data_Integration.API.BL.Services.Managers
{
    public class LeagueLocalizeServiceManager
    {
        private const string LeagueLocalizePath = "{0}/ContentFiles/LeagueLocalize/{1}";

        public static LeagueLocalize AddOrEditLeagueLocalize(string baseUrl /*, long createdBy*/,
            LeagueLocalizeRecord record, LeagueLocalize oldLeagueLocalize = null)
        {
            if (oldLeagueLocalize == null) //new leagueLocalize
            {
                oldLeagueLocalize = new LeagueLocalize();
                oldLeagueLocalize.ModificationDate = DateTime.Now;
                oldLeagueLocalize.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldLeagueLocalize.ModificationDate = DateTime.Now;
                oldLeagueLocalize.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldLeagueLocalize.Name = record.Name;
            if (record.LeagueId>0) oldLeagueLocalize.LeagueId = record.LeagueId;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldLeagueLocalize.LanguageId = record.LanguageId;        
           
            return oldLeagueLocalize;
        }

        public static IQueryable<LeagueLocalizeRecord> ApplyFilter(IQueryable<LeagueLocalizeRecord> query,
            LeagueLocalizeRecord leagueLocalizeRecord)
        {
            if (leagueLocalizeRecord.Id > 0)
                query = query.Where(c => c.Id == leagueLocalizeRecord.Id);
            //if (leagueLocalizeRecord.Valid != null && leagueLocalizeRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);
            if (leagueLocalizeRecord.LeagueId > 0)
                query = query.Where(c => c.LeagueId == leagueLocalizeRecord.LeagueId);
            //if (!string.IsNullOrWhiteSpace(leagueLocalizeRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(leagueLocalizeRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}