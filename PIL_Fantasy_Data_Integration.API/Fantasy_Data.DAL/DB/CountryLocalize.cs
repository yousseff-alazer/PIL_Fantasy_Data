using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class CountryLocalize
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public string LanguageId { get; set; }
        public string Name { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual Country Country { get; set; }
    }
}
