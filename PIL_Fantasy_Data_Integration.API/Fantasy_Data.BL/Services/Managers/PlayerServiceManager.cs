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
    public class PlayerServiceManager
    {
        public static Player AddOrEditPlayer(long? createdBy, PlayerRecord record, Player oldPlayer = null)
        {
            if (oldPlayer == null)//new player
            {
                oldPlayer = new Player();
                oldPlayer.CreationDate = DateTime.Now;
                oldPlayer.CreatedBy = createdBy;
            }
            else
            {
                oldPlayer.ModificationDate = DateTime.Now;
                oldPlayer.ModifiedBy = createdBy;
            }

            if (record.TeamId>0)
            {
                oldPlayer.TeamId = record.TeamId;
            }

            if (!string.IsNullOrWhiteSpace(record.Price))
            {
                oldPlayer.Price = record.Price;
            }
            return oldPlayer;
        }

        public static IQueryable<PlayerRecord> ApplyFilter(IQueryable<PlayerRecord> query, PlayerRequest playerRequest)
        {
            if (playerRequest.PlayerRecord != null && playerRequest.PlayerRecord.Id > 0)
                query = query.Where(c => c.Id == playerRequest.PlayerRecord.Id);

            if (playerRequest.PlayerRecord != null && playerRequest.PlayerRecord.TeamId > 0)
                query = query.Where(c => c.TeamId == playerRequest.PlayerRecord.TeamId);

            if (playerRequest.PlayerRecord?.TeamsIds != null && playerRequest.PlayerRecord.TeamsIds.Count > 0)
                query = query.Where(c => playerRequest.PlayerRecord.TeamsIds.Contains(c.TeamId));

            if (playerRequest.PlayerRecord?.Price != null)
                query = query.Where(c => playerRequest.PlayerRecord.Price.Contains(c.Price));
            return query;
        }
    }
}