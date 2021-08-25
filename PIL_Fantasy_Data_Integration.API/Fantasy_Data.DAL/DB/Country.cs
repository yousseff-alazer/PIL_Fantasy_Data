using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class Country
    {
        public Country()
        {
            CountryLocalizes = new HashSet<CountryLocalize>();
        }

        public long Id { get; set; }
        public string Iso { get; set; }
        public string Code { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string IntegrationId { get; set; }
        public int? DiffHours { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }

        public virtual ICollection<CountryLocalize> CountryLocalizes { get; set; }
    }
}
