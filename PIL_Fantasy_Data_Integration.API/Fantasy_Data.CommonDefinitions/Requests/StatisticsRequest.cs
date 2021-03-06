using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Requests
{
    public class StatisticsRequest : BaseRequest
    {
        public StatisticsRecord StatisticsRecord { get; set; }
        public string LeaguePerformId { get; set; }
    }
}