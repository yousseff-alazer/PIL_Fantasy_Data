using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class LeagueRecord
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DefaultImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }

        public IEnumerable<ItemLocalizeRecord> LeagueLocalize { get; set; }

        public List<ItemLocalizeRecord> LeagueLocalizeList { get; set; }

        public string DefaultName
        {
            get
            {
                return LeagueLocalize?.FirstOrDefault()?.Name;
            }
            set { }
        }

        [Required]
        public IEnumerable<long> OperatorsIds { get; set; }

        public string Color { get; set; }
        public string IntegrationId { get; set; }

        [Required]
        public string LeagueCountry { get; set; }

        [Required]
        public string LeagueType { get; set; }

        [Required]
        public string LeagueIsFriendly { get; set; }

        [Required]
        public string LeagueCountryCode { get; set; }

        [Required]
        public int LeagueDisplayOrder { get; set; }

        public bool IsFavorite { get; set; }
        public long VendorId { get; set; }
        public bool? IsPrediction { get; set; }
        public IEnumerable<Match> MatchesForPrediction { get; set; }
        public bool HasPrediction { get; set; }
    }
}