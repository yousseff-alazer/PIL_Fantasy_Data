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
    public class CountryServiceManager
    {
        //public static Country AddOrEditCountry(long createdBy, CountryRecord record, Country oldCountry = null)
        //{
        //    if (oldCountry == null)//new country
        //    {
        //        oldCountry = new Country();
        //        oldCountry.CreationDate = DateTime.Now;
        //        oldCountry.CreatedBy = createdBy;
        //    }
        //    else
        //    {
        //        oldCountry.ModificationDate = DateTime.Now;
        //        oldCountry.ModifiedBy = createdBy;
        //    }
        //    oldCountry.Code = record.Code;
        //    oldCountry.Iso = record.Iso;
        //    oldCountry.ContinentId = record.ContinentId;
        //    return oldCountry;
        //}

        public static IQueryable<CountryRecord> ApplyFilter(IQueryable<CountryRecord> query, CountryRecord countryRecord)
        {
            if (countryRecord.Id > 0)
                query = query.Where(c => c.Id == countryRecord.Id);
            if (countryRecord.Id > 0)
                query = query.Where(c => c.Id == countryRecord.Id);
            if (!string.IsNullOrWhiteSpace(countryRecord.Code))
                query = query.Where(c => c.Code != null && c.Code.Trim().ToLower().Contains(countryRecord.Code.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(countryRecord.Name))
                query = query.Where(c => c.Name != null && c.Name.Trim().ToLower().Contains(countryRecord.Name.Trim().ToLower()));
            if (countryRecord.Show != null)
                query = query.Where(c => c.Show != null && c.Show.Value == countryRecord.Show.Value);
            return query;
        }
    }
}
