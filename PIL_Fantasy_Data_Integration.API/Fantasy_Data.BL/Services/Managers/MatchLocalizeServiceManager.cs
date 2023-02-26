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
    public class MatchLocalizeServiceManager
    {
        private const string MatchLocalizePath = "{0}/ContentFiles/MatchLocalize/{1}";

        public static MatchLocalize AddOrEditMatchLocalize(string baseUrl /*, long createdBy*/,
            MatchLocalizeRecord record, MatchLocalize oldMatchLocalize = null)
        {
            if (oldMatchLocalize == null) //new matchLocalize
            {
                oldMatchLocalize = new MatchLocalize();
                oldMatchLocalize.CreationDate = DateTime.Now;
                oldMatchLocalize.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldMatchLocalize.ModificationDate = DateTime.Now;
                oldMatchLocalize.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Title)) oldMatchLocalize.Title = record.Title;
            if (!string.IsNullOrWhiteSpace(record.Description)) oldMatchLocalize.Description = record.Description;
            if (record.MatchId>0) oldMatchLocalize.MatchId = record.MatchId;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldMatchLocalize.LanguageId = record.LanguageId;        
           
            return oldMatchLocalize;
        }

        public static IQueryable<MatchLocalizeRecord> ApplyFilter(IQueryable<MatchLocalizeRecord> query,
            MatchLocalizeRecord matchLocalizeRecord)
        {
            if (matchLocalizeRecord.Id > 0)
                query = query.Where(c => c.Id == matchLocalizeRecord.Id);
            //if (matchLocalizeRecord.Valid != null && matchLocalizeRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);
            if (matchLocalizeRecord.MatchId > 0)
                query = query.Where(c => c.MatchId == matchLocalizeRecord.MatchId);
            //if (!string.IsNullOrWhiteSpace(matchLocalizeRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(matchLocalizeRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}