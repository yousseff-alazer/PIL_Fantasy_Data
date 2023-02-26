using Newtonsoft.Json;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class LeagueLocalizeRequest : BaseRequest
    {
        public List<LeagueLocalizeRecord> LeagueLocalizeRecords { get; set; }

        public LeagueLocalizeRecord LeagueLocalizeRecord { get; set; }
    }
}
