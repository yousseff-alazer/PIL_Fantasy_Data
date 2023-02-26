using Microsoft.AspNetCore.Http;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class PlayerRecord
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }

        //[Required]
        public long TeamId { get; set; }

        //public IEnumerable<PlayerLocalizeRecord> PlayerLocalize { get; set; }
        [JsonIgnore]
        public IEnumerable<PlayerLocalize> PlayerLocalize { get; set; }
        [JsonIgnore]
        public IEnumerable<TeamLocalize> TeamLocalize { get; set; }
        //public List<ItemLocalizeRecord> PlayerLocalizeList { get; set; }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (PlayerLocalize!=null&&PlayerLocalize.Any())
                    name = PlayerLocalize?.FirstOrDefault()?.Name;
                else
                    name = value;

            }
        }
        private string teamName;
        public string TeamName
        {
            get
            {
                return teamName;
            }
            set
            {
                if (TeamLocalize != null && TeamLocalize.Any())
                    teamName = TeamLocalize?.FirstOrDefault()?.Name;
                else
                    teamName = value;

            }
        }
        public bool? IsTopPlayer { get; set; }
        public string IntegrationId { get; set; }
        public List<long> TeamsIds { get; set; }
        //public IEnumerable<ItemLocalizeRecord> TeamLocalize { get; set; }
        public string TeamIntegrationId { get; set; }


        public bool? Injured { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Minutes { get; set; }
        public string Rating { get; set; }
        public string GoalsTotal { get; set; }
        public string GoalsAssists { get; set; }
        public string PassesTotal { get; set; }
        public string PassesAccuracy { get; set; }
        public string GoalsSaves { get; set; }
        public string CardsYellow { get; set; }
        public string CardsYellowRed { get; set; }
        public string CardsRed { get; set; }
        public string Price { get; set; }

        public bool? PositionFilter { get; set; }
        public string Credit { get; set; }

        public long? PositionId { get; set; }
        public string PositionCode { get; set; }


        //public long? MatchId { get; set; }//for filter
    }
}