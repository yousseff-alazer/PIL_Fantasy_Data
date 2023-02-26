using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses
{
    public class MatchResponse : BaseResponse
    {
        [JsonProperty("Data")]
        public List<MatchRecord> MatchRecords { get; set; }
    }
}
