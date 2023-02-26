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
    public class LeagueServiceManager
    {
    //    private const string LeaguePath = "{0}/ContentFiles/LeagueImages/{1}";

    //    public static League AddOrEditLeague(string baseUrl, long createdBy, LeagueRecord record, League oldLeague = null)
    //    {
    //        if (oldLeague == null)//new league
    //        {
    //            oldLeague = new League();
    //            oldLeague.CreationDate = DateTime.Now;
    //            oldLeague.CreatedBy = createdBy;
    //        }
    //        else
    //        {
    //            oldLeague.ModificationDate = DateTime.Now;
    //            oldLeague.ModifiedBy = createdBy;
    //        }
    //        oldLeague.EndDate = record.EndDate;
    //        oldLeague.StartDate = record.StartDate;
    //        oldLeague.Color = record.Color;
    //        oldLeague.VendorId = record.VendorId;
    //        if (record.IsPrediction != null)
    //        {
    //            oldLeague.IsPrediction = record.IsPrediction;
    //        }
    //        //oldLeague.LeagueCountry = record.LeagueCountry;
    //        //oldLeague.LeagueCountryCode = record.LeagueCountryCode;
    //        //oldLeague.LeagueIsFriendly = record.LeagueIsFriendly;
    //        //oldLeague.LeagueDisplayOrder = record.LeagueDisplayOrder;
    //        //oldLeague.LeagueType = record.LeagueType;

    //        //upload
    //        var file = record.ImageFile;
    //        if (file != null && file.Length > 0)
    //        {
    //            var fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
    //            var physicalPath = string.Format(LeaguePath, Directory.GetCurrentDirectory() + "/wwwroot", fileName);
    //            var virtualPath = string.Format(LeaguePath, baseUrl, fileName);

    //            using (var stream = new FileStream(physicalPath, FileMode.Create))
    //            {
    //                file.CopyTo(stream);
    //            }
    //            oldLeague.DefaultImageUrl = virtualPath;
    //        }

    //        return oldLeague;
    //    }

        public static IQueryable<LeagueRecord> ApplyFilter(IQueryable<LeagueRecord> query, LeagueRequest leagueRequest)
        {
            if (leagueRequest.LeagueRecord != null)
            {
                if (leagueRequest.LeagueRecord.Id > 0)
                    query = query.Where(c => c.Id == leagueRequest.LeagueRecord.Id);
                if (!string.IsNullOrWhiteSpace(leagueRequest.LeagueRecord.LeagueCountry))
                    query = query.Where(c => c.LeagueCountry!=null&& c.LeagueCountry.Trim().Contains(leagueRequest.LeagueRecord.LeagueCountry.Trim()));
                if (!string.IsNullOrWhiteSpace(leagueRequest.LeagueRecord.Name))
                    query = query.Where(c => c.Name != null && c.Name.Trim().ToLower().Contains(leagueRequest.LeagueRecord.Name.Trim().ToLower()));
                if (leagueRequest.LeagueRecord.Show!=null)
                    query = query.Where(c => c.Show!=null&& c.Show.Value== leagueRequest.LeagueRecord.Show.Value);
            }

            
            if (leagueRequest.VendorId > 0)
                query = query.Where(c => c.VendorId == leagueRequest.VendorId);
            return query;
        }
    }
}