using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class TeamLocalizeRequest : BaseRequest
    {
        public List<TeamLocalizeRecord> TeamLocalizeRecords { get; set; }

        public TeamLocalizeRecord TeamLocalizeRecord { get; set; }
    }
}
