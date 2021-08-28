using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses
{
    public class VendorResponse : BaseResponse
    {
        public List<VendorRecord> VendorRecords { get; set; }
    }
}
