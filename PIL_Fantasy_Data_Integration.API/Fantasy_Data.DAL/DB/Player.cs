using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class Player
    {
        public Player()
        {
            PlayerLocalizes = new HashSet<PlayerLocalize>();
            PlayerMatchRatings = new HashSet<PlayerMatchRating>();
            PlayerMatchStats = new HashSet<PlayerMatchStat>();
        }

        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long TeamId { get; set; }
        public bool? Injured { get; set; }
        public string IntegrationId { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Name { get; set; }
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
        public long? PositionId { get; set; }
        public string Price { get; set; }

        public virtual PlayerPosition PositionNavigation { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<PlayerLocalize> PlayerLocalizes { get; set; }
        public virtual ICollection<PlayerMatchRating> PlayerMatchRatings { get; set; }
        public virtual ICollection<PlayerMatchStat> PlayerMatchStats { get; set; }
    }
}
