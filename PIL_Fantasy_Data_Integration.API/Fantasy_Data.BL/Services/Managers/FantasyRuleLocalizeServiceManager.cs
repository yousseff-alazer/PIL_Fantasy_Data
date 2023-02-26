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
    public class FantasyRuleLocalizeServiceManager
    {
        private const string FantasyRuleLocalizePath = "{0}/ContentFiles/FantasyRuleLocalize/{1}";

        public static FantasyRuleLocalize AddOrEditFantasyRuleLocalize(string baseUrl /*, long createdBy*/,
            FantasyRuleLocalizeRecord record, FantasyRuleLocalize oldFantasyRuleLocalize = null)
        {
            if (oldFantasyRuleLocalize == null) //new fantasyRuleLocalize
            {
                oldFantasyRuleLocalize = new FantasyRuleLocalize();
                oldFantasyRuleLocalize.CreationDate = DateTime.Now;
                oldFantasyRuleLocalize.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldFantasyRuleLocalize.ModificationDate = DateTime.Now;
                oldFantasyRuleLocalize.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Title)) oldFantasyRuleLocalize.Title = record.Title;
            if (!string.IsNullOrWhiteSpace(record.Description)) oldFantasyRuleLocalize.Description = record.Description;
            if (record.FantasyRuleId>0) oldFantasyRuleLocalize.FantasyRuleId = record.FantasyRuleId;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldFantasyRuleLocalize.LanguageId = record.LanguageId;        
           
            return oldFantasyRuleLocalize;
        }

        public static IQueryable<FantasyRuleLocalizeRecord> ApplyFilter(IQueryable<FantasyRuleLocalizeRecord> query,
            FantasyRuleLocalizeRecord fantasyRuleLocalizeRecord)
        {
            if (fantasyRuleLocalizeRecord.Id > 0)
                query = query.Where(c => c.Id == fantasyRuleLocalizeRecord.Id);
            //if (fantasyRuleLocalizeRecord.Valid != null && fantasyRuleLocalizeRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            //if (!string.IsNullOrWhiteSpace(fantasyRuleLocalizeRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(fantasyRuleLocalizeRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}