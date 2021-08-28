using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Parameters
    {
        [JsonProperty("team")]
        public string Team;

        [JsonProperty("season")]
        public string Season;

        [JsonProperty("page")]
        public string Page;
    }

    public class Paging
    {
        [JsonProperty("current")]
        public int Current;

        [JsonProperty("total")]
        public int Total;
    }

    public class Birth
    {
        [JsonProperty("date")]
        public string Date;

        [JsonProperty("place")]
        public string Place;

        [JsonProperty("country")]
        public string Country;
    }

    public partial class Player
    {
        [JsonProperty("firstname")]
        public string Firstname;

        [JsonProperty("lastname")]
        public string Lastname;

        [JsonProperty("age")]
        public int? Age;

        [JsonProperty("birth")]
        public Birth Birth;

        [JsonProperty("nationality")]
        public string Nationality;

        [JsonProperty("height")]
        public string Height;

        [JsonProperty("weight")]
        public string Weight;

        [JsonProperty("injured")]
        public bool Injured;

        [JsonProperty("photo")]
        public string Photo;
    }

    public partial class Team
    {
    }

    public partial class League
    {
        [JsonProperty("flag")]
        public string Flag;

        [JsonProperty("season")]
        public int Season;
    }

    public class Games
    {
        [JsonProperty("appearences")]
        public int? Appearences;

        [JsonProperty("lineups")]
        public object Lineups;

        [JsonProperty("minutes")]
        public object Minutes;

        [JsonProperty("number")]
        public object Number;

        [JsonProperty("position")]
        public string Position;

        [JsonProperty("rating")]
        public object Rating;

        [JsonProperty("captain")]
        public bool Captain;
    }

    public class Substitutes
    {
        [JsonProperty("in")]
        public object In;

        [JsonProperty("out")]
        public object Out;

        [JsonProperty("bench")]
        public object Bench;
    }

    public class Shots
    {
        [JsonProperty("total")]
        public object Total;

        [JsonProperty("on")]
        public object On;
    }

    public partial class Goals
    {
        [JsonProperty("total")]
        public int? Total;

        [JsonProperty("conceded")]
        public object Conceded;

        [JsonProperty("assists")]
        public object Assists;

        [JsonProperty("saves")]
        public object Saves;
    }

    public class Passes
    {
        [JsonProperty("total")]
        public object Total;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("accuracy")]
        public object Accuracy;
    }

    public class Tackles
    {
        [JsonProperty("total")]
        public object Total;

        [JsonProperty("blocks")]
        public object Blocks;

        [JsonProperty("interceptions")]
        public object Interceptions;
    }

    public class Duels
    {
        [JsonProperty("total")]
        public object Total;

        [JsonProperty("won")]
        public object Won;
    }

    public class Dribbles
    {
        [JsonProperty("attempts")]
        public object Attempts;

        [JsonProperty("success")]
        public object Success;

        [JsonProperty("past")]
        public object Past;
    }

    public class Fouls
    {
        [JsonProperty("drawn")]
        public object Drawn;

        [JsonProperty("committed")]
        public object Committed;
    }

    public class Cards
    {
        [JsonProperty("yellow")]
        public object Yellow;

        [JsonProperty("yellowred")]
        public object Yellowred;

        [JsonProperty("red")]
        public object Red;
    }

    public class Penalty
    {
        [JsonProperty("won")]
        public object Won;

        [JsonProperty("commited")]
        public object Commited;

        [JsonProperty("scored")]
        public object Scored;

        [JsonProperty("missed")]
        public object Missed;

        [JsonProperty("saved")]
        public object Saved;
    }

    public class Statistic
    {
        [JsonProperty("team")]
        public Team Team;

        [JsonProperty("league")]
        public League League;

        [JsonProperty("games")]
        public Games Games;

        [JsonProperty("substitutes")]
        public Substitutes Substitutes;

        [JsonProperty("shots")]
        public Shots Shots;

        [JsonProperty("goals")]
        public Goals Goals;

        [JsonProperty("passes")]
        public Passes Passes;

        [JsonProperty("tackles")]
        public Tackles Tackles;

        [JsonProperty("duels")]
        public Duels Duels;

        [JsonProperty("dribbles")]
        public Dribbles Dribbles;

        [JsonProperty("fouls")]
        public Fouls Fouls;

        [JsonProperty("cards")]
        public Cards Cards;

        [JsonProperty("penalty")]
        public Penalty Penalty;
    }

    public class Response
    {
        [JsonProperty("player")]
        public Player Player;

        [JsonProperty("statistics")]
        public List<Statistic> Statistics;
    }

    public class FootBallPlayersRes
    {
        [JsonProperty("get")]
        public string Get;

        [JsonProperty("parameters")]
        public Parameters Parameters;

        [JsonProperty("errors")]
        public List<object> Errors;

        [JsonProperty("results")]
        public int Results;

        [JsonProperty("paging")]
        public Paging Paging;

        [JsonProperty("response")]
        public List<Response> Response;
    }
}