using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class League
    {
        public League()
        {
            LeagueLocalizes = new HashSet<LeagueLocalize>();
            Matches = new HashSet<Match>();
            Teams = new HashSet<Team>();
        }

        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DefaultImageUrl { get; set; }
        public string Color { get; set; }
        public string IntegrationId { get; set; }
        public string LeagueCountry { get; set; }
        public string LeagueCountryCode { get; set; }
        public int? LeagueDisplayOrder { get; set; }
        public string LeagueIsFriendly { get; set; }
        public string LeagueType { get; set; }
        public long? VendorId { get; set; }
        public bool? Show { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LeagueLocalize> LeagueLocalizes { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
