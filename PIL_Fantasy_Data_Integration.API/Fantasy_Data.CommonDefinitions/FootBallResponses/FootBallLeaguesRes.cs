//using System;
//using System.Collections.Generic;
//using System.Text;
//using Newtonsoft.Json;

//namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses
//{
//    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
//    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
//    public partial class League
//    {
//        [JsonProperty("id")]
//        public int Id;

//        [JsonProperty("name")]
//        public string Name;

//        [JsonProperty("type")]
//        public string Type;

//        [JsonProperty("logo")]
//        public string Logo;
//    }

//    public class Country
//    {
//        [JsonProperty("name")]
//        public string Name;

//        [JsonProperty("code")]
//        public string Code;

//        [JsonProperty("flag")]
//        public string Flag;
//    }

//    public class Fixtures
//    {
//        [JsonProperty("events")]
//        public bool PIL_Fantasy_Data_Integration.APIs;

//        [JsonProperty("lineups")]
//        public bool Lineups;

//        [JsonProperty("statistics_fixtures")]
//        public bool StatisticsFixtures;

//        [JsonProperty("statistics_players")]
//        public bool StatisticsPlayers;
//    }

//    public class Coverage
//    {
//        [JsonProperty("fixtures")]
//        public Fixtures Fixtures;

//        [JsonProperty("standings")]
//        public bool Standings;

//        [JsonProperty("players")]
//        public bool Players;

//        [JsonProperty("top_scorers")]
//        public bool TopScorers;

//        [JsonProperty("predictions")]
//        public bool Predictions;

//        [JsonProperty("odds")]
//        public bool Odds;
//    }

//    public class Season
//    {
//        [JsonProperty("year")]
//        public int Year;

//        [JsonProperty("start")]
//        public string Start;

//        [JsonProperty("end")]
//        public string End;

//        [JsonProperty("current")]
//        public bool Current;

//        [JsonProperty("coverage")]
//        public Coverage Coverage;
//    }

//    public class FootBallLeague
//    {
//        [JsonProperty("league")]
//        public League League;

//        [JsonProperty("country")]
//        public Country Country;

//        [JsonProperty("seasons")]
//        public List<Season> Seasons;
//    }
//}