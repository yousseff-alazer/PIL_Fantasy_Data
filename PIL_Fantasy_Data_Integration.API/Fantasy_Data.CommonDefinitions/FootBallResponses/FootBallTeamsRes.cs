using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses
{
    public class Team2
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("logo")]
        public string Logo;
    }

    public partial class Goals
    {
        [JsonProperty("for")]
        public int For;

        [JsonProperty("against")]
        public int Against;
    }

    public class All
    {
        [JsonProperty("played")]
        public int Played;

        [JsonProperty("win")]
        public int Win;

        [JsonProperty("draw")]
        public int Draw;

        [JsonProperty("lose")]
        public int Lose;

        [JsonProperty("goals")]
        public Goals Goals;
    }

    public class Goals2
    {
        [JsonProperty("for")]
        public int For;

        [JsonProperty("against")]
        public int Against;
    }

    public class Home
    {
        [JsonProperty("played")]
        public int Played;

        [JsonProperty("win")]
        public int Win;

        [JsonProperty("draw")]
        public int Draw;

        [JsonProperty("lose")]
        public int Lose;

        [JsonProperty("goals")]
        public Goals2 Goals;
    }

    public class Goals3
    {
        [JsonProperty("for")]
        public int For;

        [JsonProperty("against")]
        public int Against;
    }

    public class Away
    {
        [JsonProperty("played")]
        public int Played;

        [JsonProperty("win")]
        public int Win;

        [JsonProperty("draw")]
        public int Draw;

        [JsonProperty("lose")]
        public int Lose;

        [JsonProperty("goals")]
        public Goals3 Goals;
    }

    public class FootBallTeamsRes
    {
        [JsonProperty("rank")]
        public int Rank;

        [JsonProperty("team")]
        public Team2 Team;

        [JsonProperty("points")]
        public int Points;

        [JsonProperty("goalsDiff")]
        public int GoalsDiff;

        [JsonProperty("group")]
        public string Group;

        [JsonProperty("form")]
        public string Form;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("all")]
        public All All;

        [JsonProperty("home")]
        public Home Home;

        [JsonProperty("away")]
        public Away Away;

        [JsonProperty("update")]
        public DateTime Update;
    }
}