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
    public class PlayerLocalizeServiceManager
    {
        private const string PlayerLocalizePath = "{0}/ContentFiles/PlayerLocalize/{1}";

        public static PlayerLocalize AddOrEditPlayerLocalize(string baseUrl /*, long createdBy*/,
            PlayerLocalizeRecord record, PlayerLocalize oldPlayerLocalize = null)
        {
            if (oldPlayerLocalize == null) //new playerLocalize
            {
                oldPlayerLocalize = new PlayerLocalize();
                oldPlayerLocalize.ModificationDate = DateTime.Now;
                oldPlayerLocalize.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldPlayerLocalize.ModificationDate = DateTime.Now;
                oldPlayerLocalize.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldPlayerLocalize.Name = record.Name;
            if (record.PlayerId>0) oldPlayerLocalize.PlayerId = record.PlayerId;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldPlayerLocalize.LanguageId = record.LanguageId;        
           
            return oldPlayerLocalize;
        }

        public static IQueryable<PlayerLocalizeRecord> ApplyFilter(IQueryable<PlayerLocalizeRecord> query,
            PlayerLocalizeRecord playerLocalizeRecord)
        {
            if (playerLocalizeRecord.Id > 0)
                query = query.Where(c => c.Id == playerLocalizeRecord.Id);
            //if (playerLocalizeRecord.Valid != null && playerLocalizeRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);
            if (playerLocalizeRecord.PlayerId > 0)
                query = query.Where(c => c.PlayerId == playerLocalizeRecord.PlayerId);
            //if (!string.IsNullOrWhiteSpace(playerLocalizeRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(playerLocalizeRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}