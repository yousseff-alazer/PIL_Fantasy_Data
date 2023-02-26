using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class Match
    {
        public Match()
        {
            MatchLocalizes = new HashSet<MatchLocalize>();
            PlayerMatchRatings = new HashSet<PlayerMatchRating>();
            PlayerMatchStats = new HashSet<PlayerMatchStat>();
        }

        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long Team1Id { get; set; }
        public long Team2Id { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime? EndDatetime { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public string IntegrationId { get; set; }
        public long HomeTeamId { get; set; }
        public int Week { get; set; }
        public long LeagueId { get; set; }
        public string Status { get; set; }
        public long VendorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsSync { get; set; }

        public virtual League League { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
        public virtual ICollection<MatchLocalize> MatchLocalizes { get; set; }
        public virtual ICollection<PlayerMatchRating> PlayerMatchRatings { get; set; }
        public virtual ICollection<PlayerMatchStat> PlayerMatchStats { get; set; }
    }
}
