using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Periods
    {
        [JsonProperty("first")]
        public int? First;

        [JsonProperty("second")]
        public int? Second;
    }

    public class Venue
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("city")]
        public string City;
    }

    public class Status
    {
        [JsonProperty("long")]
        public string Long;

        [JsonProperty("short")]
        public string Short;

        [JsonProperty("elapsed")]
        public int? Elapsed;
    }

    public class Fixture
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("referee")]
        public object Referee;

        [JsonProperty("timezone")]
        public string Timezone;

        [JsonProperty("date")]
        public DateTime Date;

        [JsonProperty("timestamp")]
        public int Timestamp;

        [JsonProperty("periods")]
        public Periods Periods;

        [JsonProperty("venue")]
        public Venue Venue;

        [JsonProperty("status")]
        public Status Status;
    }

    public class League3
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("country")]
        public string Country;

        [JsonProperty("logo")]
        public string Logo;

        [JsonProperty("flag")]
        public string Flag;

        [JsonProperty("season")]
        public int Season;

        [JsonProperty("round")]
        public string Round;
    }

    public class Home2
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("logo")]
        public string Logo;

        [JsonProperty("winner")]
        public bool? Winner;
    }

    public class Away2
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("logo")]
        public string Logo;

        [JsonProperty("winner")]
        public bool? Winner;
    }

    public class Teams
    {
        [JsonProperty("home")]
        public Home2 Home;

        [JsonProperty("away")]
        public Away2 Away;
    }

    public class Goals5
    {
        [JsonProperty("home")]
        public int? Home;

        [JsonProperty("away")]
        public int? Away;
    }

    public class Halftime
    {
        [JsonProperty("home")]
        public int? Home;

        [JsonProperty("away")]
        public int? Away;
    }

    public class Fulltime
    {
        [JsonProperty("home")]
        public int? Home;

        [JsonProperty("away")]
        public int? Away;
    }

    public class Extratime
    {
        [JsonProperty("home")]
        public object Home;

        [JsonProperty("away")]
        public object Away;
    }

    public class Penalty2
    {
        [JsonProperty("home")]
        public object Home;

        [JsonProperty("away")]
        public object Away;
    }

    public class Score
    {
        [JsonProperty("halftime")]
        public Halftime Halftime;

        [JsonProperty("fulltime")]
        public Fulltime Fulltime;

        [JsonProperty("extratime")]
        public Extratime Extratime;

        [JsonProperty("penalty")]
        public Penalty2 Penalty;
    }

    public class Time
    {
        [JsonProperty("elapsed")]
        public int Elapsed;

        [JsonProperty("extra")]
        public int? Extra;
    }

    public partial class Team
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("logo")]
        public string Logo;
    }

    public partial class Player
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;
    }

    public class Assist
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("name")]
        public object Name;
    }

    public class Event
    {
        [JsonProperty("time")]
        public Time Time;

        [JsonProperty("team")]
        public Team Team;

        [JsonProperty("player")]
        public Player Player;

        [JsonProperty("assist")]
        public Assist Assist;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("detail")]
        public string Detail;

        [JsonProperty("comments")]
        public object Comments;
    }

    public class FootBallMatchesRes
    {
        [JsonProperty("fixture")]
        public Fixture Fixture;

        [JsonProperty("league")]
        public League3 League;

        [JsonProperty("teams")]
        public Teams Teams;

        [JsonProperty("goals")]
        public Goals5 Goals;

        [JsonProperty("score")]
        public Score Score;

        [JsonProperty("events")]
        public List<Event> Events;

        [JsonProperty("lineups")]
        public List<object> Lineups;

        [JsonProperty("statistics")]
        public List<object> Statistics;

        [JsonProperty("players")]
        public List<object> Players;
    }
}