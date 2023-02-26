using Microsoft.AspNetCore.Http;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class TeamRecord
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }

        public int? Points { get; set; }

        //[Required]
        public long LeagueId { get; set; }

        //public IEnumerable<ItemLocalizeRecord> TeamLocalize { get; set; }

        //public List<ItemLocalizeRecord> TeamLocalizeList { get; set; }

        //public string DefaultName
        //{
        //    get
        //    {
        //        return TeamLocalize?.FirstOrDefault()?.Name;
        //    }
        //    set { }
        //}

        public string ImageUrl { get; set; }
        public int? OrderInLeague { get; set; }
        public int? WonCount { get; set; }
        public int? LossCount { get; set; }
        public int? DrawCount { get; set; }
        public string Group { get; set; }

        public IFormFile ImageFile { get; set; }
        public int? PlayedCount { get; set; }
        public int? GoalsAgainst { get; set; }
        public int? GoalsFor { get; set; }
        public string IntegrationId { get; set; }
        public List<string> TeamIntegrationId { get; set; }
        public string Team2IntegrationId { get; set; }
        //public IEnumerable<ItemLocalizeRecord> LeagueLocalize { get; set; }
        public List<string> LeagueIntegrationIds { get; set; }
        public string LeagueIntegrationId { get; set; }
        //public string Name { get; set; }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (TeamLocalize != null && TeamLocalize.Any())
                    name = TeamLocalize?.FirstOrDefault()?.Name;
                else
                    name = value;
                 
            }
        }
        [JsonIgnore]
        public IEnumerable<TeamLocalize> TeamLocalize { get;  set; }
    }
}