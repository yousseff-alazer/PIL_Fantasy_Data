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
    public class PlayerMatchStatServiceManager
    {
       
        public static IQueryable<PlayerMatchStatRecord> ApplyFilter(IQueryable<PlayerMatchStatRecord> query, PlayerMatchStatRequest playerMatchStatRequest)
        {
            if (playerMatchStatRequest.PlayerMatchStatRecord != null && playerMatchStatRequest.PlayerMatchStatRecord.Id > 0)
                query = query.Where(c => c.Id == playerMatchStatRequest.PlayerMatchStatRecord.Id);

            if (playerMatchStatRequest.PlayerMatchStatRecord != null && playerMatchStatRequest.PlayerMatchStatRecord.TeamId > 0)
                query = query.Where(c => c.TeamId == playerMatchStatRequest.PlayerMatchStatRecord.TeamId);
            if (playerMatchStatRequest.PlayerMatchStatRecord != null && playerMatchStatRequest.PlayerMatchStatRecord.MatchId > 0)
                query = query.Where(c => c.MatchId == playerMatchStatRequest.PlayerMatchStatRecord.MatchId);
            if (playerMatchStatRequest.PlayerMatchStatRecord != null && playerMatchStatRequest.PlayerMatchStatRecord.PlayerId > 0)
                query = query.Where(c => c.PlayerId == playerMatchStatRequest.PlayerMatchStatRecord.PlayerId);
            return query;
        }
    }
}