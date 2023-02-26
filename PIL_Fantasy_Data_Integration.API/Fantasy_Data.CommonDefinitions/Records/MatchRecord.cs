using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class MatchRecord
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }

        //public IEnumerable<ItemLocalizeRecord> MatchLocalize { get; set; }

        //public List<ItemLocalizeRecord> MatchLocalizeList { get; set; }

        //public string DefaultTitle
        //{
        //    get
        //    {
        //        return MatchLocalize?.FirstOrDefault()?.Title;
        //    }
        //    set { }
        //}

        public long Team2Id { get; set; }

        public long Team1Id { get; set; }
        public DateTime StartDatetime { get; set; }

        public DateTime? EndDatetime { get; set; }
        //public IEnumerable<ItemLocalizeRecord> Team1Localize { get; set; }
        //public IEnumerable<ItemLocalizeRecord> Team2Localize { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }

        public long LeagueId { get; set; }

        public string Team1ImageUrl { get; set; }
        public string Team2ImageUrl { get; set; }
        public long HomeTeamId { get; set; }
        public int Week { get; set; }
        public string IntegrationId { get; set; }
        public int DisplayOrder { get; set; }
        public long VendorId { get; set; }
        public string Status { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        //public bool IsActiveLeague { get; set; }
        public string Team1IntegrationId { get; set; }
        public string Team2IntegrationId { get; set; }
        public string LeagueIntegrationId { get; set; }
        public int Day { get; set; }
       
        public List<long> LeagueIds { get; set; }

        public int LeagueDisplayOrder { get; set; }
       
       
        public bool? Current { get; set; }
        public string LeagueImageUrl { get; set; }
        public string LeagueName { get; set; }
        public bool? IsSync { get; set; }
        public bool EndToday { get; set; }
        public ICollection<PlayerMatchRating> PlayerMatchRatings { get; set; }
        public string Prize { get; set; }
       
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Team1Localize != null && Team1Localize.Any()&& Team2Localize != null && Team2Localize.Any())
                    name = Team1Localize?.FirstOrDefault()?.Name + " vs " + Team2Localize?.FirstOrDefault()?.Name;
                else
                    name = value;

            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (MatchLocalize != null && MatchLocalize.Any())
                    description = MatchLocalize?.FirstOrDefault()?.Description;
                else
                    description = value;

            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (MatchLocalize != null && MatchLocalize.Any())
                    title = MatchLocalize?.FirstOrDefault()?.Title;
                else
                    title = value;

            }
        }

        private string team1Name;
        public string Team1Name
        {
            get
            {
                return team1Name;
            }
            set
            {
                if (Team1Localize != null && Team1Localize.Any())
                    team1Name = Team1Localize?.FirstOrDefault()?.Name;
                else
                    team1Name = value;

            }
        }
        private string team2Name;
        public string Team2Name
        {
            get
            {
                return team2Name;
            }
            set
            {
                if (Team2Localize != null && Team2Localize.Any())
                    team2Name = Team2Localize?.FirstOrDefault()?.Name;
                else
                    team2Name = value;

            }
        }
        [JsonIgnore]
        public IEnumerable<TeamLocalize> Team1Localize { get;  set; }
        [JsonIgnore]
        public IEnumerable<TeamLocalize> Team2Localize { get;  set; }
        [JsonIgnore]
        public IEnumerable<MatchLocalize> MatchLocalize { get; set; }
        public bool NotPlayed { get;  set; }
        public bool NoPoints { get; set; }
        public bool Started { get;  set; }
    }
}