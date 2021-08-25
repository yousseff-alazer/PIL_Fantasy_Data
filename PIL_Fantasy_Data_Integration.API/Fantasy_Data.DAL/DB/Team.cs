using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class Team
    {
        public Team()
        {
            MatchTeam1s = new HashSet<Match>();
            MatchTeam2s = new HashSet<Match>();
            PlayerMatchRatings = new HashSet<PlayerMatchRating>();
            PlayerMatchStats = new HashSet<PlayerMatchStat>();
            Players = new HashSet<Player>();
            TeamLocalizes = new HashSet<TeamLocalize>();
        }

        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long LeagueId { get; set; }
        public int Points { get; set; }
        public string ImageUrl { get; set; }
        public int OrderInLeague { get; set; }
        public int WonCount { get; set; }
        public int LossCount { get; set; }
        public int DrawCount { get; set; }
        public string Group { get; set; }
        public string IntegrationId { get; set; }
        public int? PlayedCount { get; set; }
        public int? GoalsFor { get; set; }
        public int? GoalsAgainst { get; set; }
        public string Name { get; set; }

        public virtual League League { get; set; }
        public virtual ICollection<Match> MatchTeam1s { get; set; }
        public virtual ICollection<Match> MatchTeam2s { get; set; }
        public virtual ICollection<PlayerMatchRating> PlayerMatchRatings { get; set; }
        public virtual ICollection<PlayerMatchStat> PlayerMatchStats { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<TeamLocalize> TeamLocalizes { get; set; }
    }
}
