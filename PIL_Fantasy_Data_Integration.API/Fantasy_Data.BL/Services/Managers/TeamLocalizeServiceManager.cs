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
    public class TeamLocalizeServiceManager
    {
        private const string TeamLocalizePath = "{0}/ContentFiles/TeamLocalize/{1}";

        public static TeamLocalize AddOrEditTeamLocalize(string baseUrl /*, long createdBy*/,
            TeamLocalizeRecord record, TeamLocalize oldTeamLocalize = null)
        {
            if (oldTeamLocalize == null) //new teamLocalize
            {
                oldTeamLocalize = new TeamLocalize();
                oldTeamLocalize.CreationDate = DateTime.Now;
                oldTeamLocalize.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldTeamLocalize.ModificationDate = DateTime.Now;
                oldTeamLocalize.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldTeamLocalize.Name = record.Name;
            if (record.TeamId>0) oldTeamLocalize.TeamId = record.TeamId;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldTeamLocalize.LanguageId = record.LanguageId;        
           
            return oldTeamLocalize;
        }

        public static IQueryable<TeamLocalizeRecord> ApplyFilter(IQueryable<TeamLocalizeRecord> query,
            TeamLocalizeRecord teamLocalizeRecord)
        {
            if (teamLocalizeRecord.Id > 0)
                query = query.Where(c => c.Id == teamLocalizeRecord.Id);
            //if (teamLocalizeRecord.Valid != null && teamLocalizeRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);
            if (teamLocalizeRecord.TeamId > 0)
                query = query.Where(c => c.TeamId == teamLocalizeRecord.TeamId);
            //if (!string.IsNullOrWhiteSpace(teamLocalizeRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(teamLocalizeRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}