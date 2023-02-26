using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses
{
    public class MatchContestPrizeResponse 
    {
        [JsonPropertyName("id")]
        public string Id;

        [JsonPropertyName("prize")]
        public string Prize;
    }
    public class Rbase
    {
        [JsonPropertyName("status")]
        public bool Status;

        [JsonPropertyName("message")]
        public string Message;

        [JsonPropertyName("data")]
        public List<MatchContestPrizeResponse> Data;
    }


}
