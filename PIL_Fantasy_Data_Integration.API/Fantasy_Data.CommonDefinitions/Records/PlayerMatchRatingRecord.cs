using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class PlayerMatchRatingRecord
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long TeamId { get; set; }
        public long PlayerId { get; set; }
        public long MatchId { get; set; }
        public string IntegrationId { get; set; }
        public string Minutes { get; set; }
        public string Rating { get; set; }

    }
}