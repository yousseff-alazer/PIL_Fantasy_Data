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
    public class PlayerMatchRatingServiceManager
    {
       
        public static IQueryable<PlayerMatchRatingRecord> ApplyFilter(IQueryable<PlayerMatchRatingRecord> query, PlayerMatchRatingRequest playerMatchRatingRequest)
        {
            if (playerMatchRatingRequest.PlayerMatchRatingRecord != null && playerMatchRatingRequest.PlayerMatchRatingRecord.Id > 0)
                query = query.Where(c => c.Id == playerMatchRatingRequest.PlayerMatchRatingRecord.Id);

            if (playerMatchRatingRequest.PlayerMatchRatingRecord != null && playerMatchRatingRequest.PlayerMatchRatingRecord.TeamId > 0)
                query = query.Where(c => c.TeamId == playerMatchRatingRequest.PlayerMatchRatingRecord.TeamId);
            if (playerMatchRatingRequest.PlayerMatchRatingRecord != null && playerMatchRatingRequest.PlayerMatchRatingRecord.MatchId > 0)
                query = query.Where(c => c.MatchId == playerMatchRatingRequest.PlayerMatchRatingRecord.MatchId);
            if (playerMatchRatingRequest.PlayerMatchRatingRecord != null && playerMatchRatingRequest.PlayerMatchRatingRecord.PlayerId > 0)
                query = query.Where(c => c.PlayerId == playerMatchRatingRequest.PlayerMatchRatingRecord.PlayerId);

            return query;
        }
    }
}