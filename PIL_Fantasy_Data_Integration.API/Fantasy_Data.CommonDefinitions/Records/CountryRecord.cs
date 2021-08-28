using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class CountryRecord
    {
        public long Id { get; set; }

        [Required]
        public string Iso { get; set; }

        public string Code { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? ModificationDate { get; set; }

        public long? ModifiedBy { get; set; }

        [Required]
        public long ContinentId { get; set; }

        public IEnumerable<ItemLocalizeRecord> CountryLocalize { get; set; }

        public List<ItemLocalizeRecord> CountryLocalizeList { get; set; }

        public string DefaultName
        {
            get
            {
                return CountryLocalize?.FirstOrDefault()?.Name;
            }
            set { }
        }

    }
}
