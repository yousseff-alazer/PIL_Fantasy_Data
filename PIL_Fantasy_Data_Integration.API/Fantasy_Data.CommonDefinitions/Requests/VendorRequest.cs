using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class VendorRequest : BaseRequest
    {
        public VendorRecord VendorRecord { get; set; }
    }
}
