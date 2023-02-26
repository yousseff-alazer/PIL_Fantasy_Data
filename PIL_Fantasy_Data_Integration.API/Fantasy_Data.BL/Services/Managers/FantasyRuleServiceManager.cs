using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PIL_Fantasy_Data_Integration.API.BL.Services.Managers
{
    public class FantasyRuleServiceManager
    {
        public static FantasyRule AddOrEditFantasyRule(long? createdBy, FantasyRuleRecord record, FantasyRule oldFantasyRule = null)
        {
            if (oldFantasyRule == null)//new fantasyRule
            {
                oldFantasyRule = new FantasyRule();
                oldFantasyRule.CreationDate = DateTime.Now;
                oldFantasyRule.CreatedBy = createdBy;
            }
            else
            {
                oldFantasyRule.ModificationDate = DateTime.Now;
                oldFantasyRule.ModifiedBy = createdBy;
            }
            if (!string.IsNullOrWhiteSpace(oldFantasyRule.Title))
            {
                oldFantasyRule.Title = record.Title;
            }

            if (!string.IsNullOrWhiteSpace(oldFantasyRule.Message))
            {
                oldFantasyRule.Message = record.Message;
            }

            if (!string.IsNullOrWhiteSpace(oldFantasyRule.Description))
            {
                oldFantasyRule.Description = record.Description;
            }
            return oldFantasyRule;
        }

        public static IQueryable<FantasyRuleRecord> ApplyFilter(IQueryable<FantasyRuleRecord> query, FantasyRuleRecord fantasyRuleRecord)
        {
            if (fantasyRuleRecord.Id > 0)
                query = query.Where(c => c.Id == fantasyRuleRecord.Id);
            if (!string.IsNullOrWhiteSpace(fantasyRuleRecord.Message))
                query = query.Where(c => c.Message != null && c.Message.Trim().ToLower().Contains(fantasyRuleRecord.Message.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(fantasyRuleRecord.Title))
                query = query.Where(c => c.Title != null && c.Title.Trim().ToLower().Contains(fantasyRuleRecord.Title.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(fantasyRuleRecord.Description))
                query = query.Where(c => c.Description != null && c.Description.Trim().ToLower().Contains(fantasyRuleRecord.Description.Trim().ToLower()));
            return query;
        }
    }
}
