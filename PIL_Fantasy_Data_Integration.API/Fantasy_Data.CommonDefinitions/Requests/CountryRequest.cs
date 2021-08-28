using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class CountryRequest : BaseRequest
    {
        public CountryRecord CountryRecord { get; set; }
    }
}
