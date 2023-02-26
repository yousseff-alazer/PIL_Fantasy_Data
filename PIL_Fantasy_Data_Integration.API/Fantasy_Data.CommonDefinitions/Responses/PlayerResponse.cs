using PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.Responses
{
    public class PlayerResponse : BaseResponse
    {
        //[JsonProperty("Data")]

        [JsonProperty("Data")]
        public PlayerRes PlayerResRecords { get; set; }

    }

    public class PlayerRes
    {
        public List<PlayerRecord> PlayerRecords { get; set; }
        public List<PlayerRecord> GK { get; set; }
        public List<PlayerRecord> DF { get; set; }
        public List<PlayerRecord> MF { get; set; }
        public List<PlayerRecord> FW { get; set; }
        public List<FantasyRule> FW_Rule { get; set; }
        public List<FantasyRule> GK_Rule { get; set; }
        public List<FantasyRule> DF_Rule { get; set; }
        public List<FantasyRule> MF_Rule { get; set; }
        public List<FantasyRule> Rules { get; set; }
    }
}
