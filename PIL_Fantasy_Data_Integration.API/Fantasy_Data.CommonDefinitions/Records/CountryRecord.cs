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

        public string Iso { get; set; }

        public string Code { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? ModificationDate { get; set; }

        public long? ModifiedBy { get; set; }

        //public IEnumerable<ItemLocalizeRecord> CountryLocalize { get; set; }

        //public List<ItemLocalizeRecord> CountryLocalizeList { get; set; }

        //public string DefaultName
        //{
        //    get
        //    {
        //        return CountryLocalize?.FirstOrDefault()?.Name;
        //    }
        //    set { }
        //}
        public string IntegrationId { get; set; }
        public int? DiffHours { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public bool? Show { get; set; }

    }
}
