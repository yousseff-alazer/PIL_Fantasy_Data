using PIL_Fantasy_Data_Integration.API.Fantasy_Data.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records
{
    public class VendorRecord
    {
        public long Id { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? ModificationDate { get; set; }

        public long? CreatedBy { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
