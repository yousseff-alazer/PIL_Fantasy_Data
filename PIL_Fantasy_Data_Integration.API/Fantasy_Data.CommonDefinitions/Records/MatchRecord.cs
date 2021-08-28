using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public IEnumerable<ItemLocalizeRecord> MatchLocalize { get; set; }

        public List<ItemLocalizeRecord> MatchLocalizeList { get; set; }

        public string DefaultTitle
        {
            get
            {
                return MatchLocalize?.FirstOrDefault()?.Title;
            }
            set { }
        }

        [Required]
        public long Team2Id { get; set; }

        [Required]
        public long Team1Id { get; set; }

        [Required]
        public DateTime StartDatetime { get; set; }

        public DateTime? EndDatetime { get; set; }
        public IEnumerable<ItemLocalizeRecord> Team1Localize { get; set; }
        public IEnumerable<ItemLocalizeRecord> Team2Localize { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }

        [Required]
        public long LeagueId { get; set; }

        public string Team1ImageUrl { get; set; }
        public string Team2ImageUrl { get; set; }

        public List<DateTime?> ActionDatetimes { get; set; }

        public List<long> ActionsIds { get; set; }
        public List<long> MatchActionsIds { get; set; }

        public List<long?> ActionsPlayersIds { get; set; }
        public List<string> ActionTimeMinSecs { get; set; }
        public List<int?> ActionTimeMins { get; set; }

        public long HomeTeamId { get; set; }
        public int Week { get; set; }
        public string IntegrationId { get; set; }
        public int DisplayOrder { get; set; }
        public long VendorId { get; set; }
        public string Status { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public bool IsActiveLeague { get; set; }
        public string Team1IntegrationId { get; set; }
        public string Team2IntegrationId { get; set; }
        public string LeagueIntegrationId { get; set; }
        public int Day { get; set; }

        public long? WeightId { get; set; }
        public int? PrizeHomeWin { get; set; }
        public int? PrizeAwayWin { get; set; }
        public int? PrizeDraw { get; set; }
        public bool? IsPrediction { get; set; }
        public List<long> LeagueIds { get; set; }
        public int PrizePackage { get; set; }
        public int? Prize { get; set; }
        public int LeagueDisplayOrder { get; set; }
    }
}