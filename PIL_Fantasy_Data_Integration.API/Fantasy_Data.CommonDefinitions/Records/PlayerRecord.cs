using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        [Required]
        public long TeamId { get; set; }

        public IEnumerable<ItemLocalizeRecord> PlayerLocalize { get; set; }

        public List<ItemLocalizeRecord> PlayerLocalizeList { get; set; }

        public string DefaultName
        {
            get
            {
                return PlayerLocalize?.FirstOrDefault()?.Name;
            }
            set { }
        }

        public bool? IsTopPlayer { get; set; }
        public string IntegrationId { get; set; }
        public List<long> TeamsIds { get; set; }
        public IEnumerable<ItemLocalizeRecord> TeamLocalize { get; set; }
        public string TeamIntegrationId { get; set; }
    }
}