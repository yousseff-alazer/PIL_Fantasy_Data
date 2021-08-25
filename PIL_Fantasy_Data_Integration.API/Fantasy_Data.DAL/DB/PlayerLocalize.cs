using System;
using System.Collections.Generic;

#nullable disable

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB
{
    public partial class PlayerLocalize
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string LanguageId { get; set; }
        public string Name { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string Type { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Foot { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Player Player { get; set; }
    }
}
