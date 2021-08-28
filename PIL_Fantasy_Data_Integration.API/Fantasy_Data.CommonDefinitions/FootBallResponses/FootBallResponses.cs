using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses
{
    public class FootBallResponses
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class Paging
        {
            [JsonProperty("current", NullValueHandling = NullValueHandling.Ignore)]
            public int Current { get; set; }

            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int Total { get; set; }
        }

        public class Response
        {
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
            public string Code { get; set; }

            [JsonProperty("flag", NullValueHandling = NullValueHandling.Ignore)]
            public string Flag { get; set; }

            [JsonProperty("league", NullValueHandling = NullValueHandling.Ignore)]
            public League League { get; set; }

            [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
            public Country Country { get; set; }

            [JsonProperty("seasons", NullValueHandling = NullValueHandling.Ignore)]
            public List<Season> Seasons { get; set; }

            [JsonProperty("player", NullValueHandling = NullValueHandling.Ignore)]
            public Player Player { get; set; }

            [JsonProperty("statistics", NullValueHandling = NullValueHandling.Ignore)]
            public List<Statistic> Statistics { get; set; }
        }

        public class Root
        {
            [JsonProperty("get", NullValueHandling = NullValueHandling.Ignore)]
            public string Get { get; set; }

            [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
            public int Results { get; set; }

            [JsonProperty("paging", NullValueHandling = NullValueHandling.Ignore)]
            public Paging Paging { get; set; }

            [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
            public List<Response> Response { get; set; }
        }

        public class League
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public int Id { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
            public string Type { get; set; }

            [JsonProperty("logo", NullValueHandling = NullValueHandling.Ignore)]
            public string Logo { get; set; }
           

            [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
            public string Country { get; set; }


            [JsonProperty("flag", NullValueHandling = NullValueHandling.Ignore)]
            public string Flag { get; set; }

            [JsonProperty("season", NullValueHandling = NullValueHandling.Ignore)]
            public int Season { get; set; }

            [JsonProperty("standings", NullValueHandling = NullValueHandling.Ignore)]
            public List<List<Standing>> Standings { get; set; }
        }

        public class Country
        {
            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
            public string Code { get; set; }

            [JsonProperty("flag", NullValueHandling = NullValueHandling.Ignore)]
            public string Flag { get; set; }
        }

        public class Fixtures
        {
            [JsonProperty("events", NullValueHandling = NullValueHandling.Ignore)]
            public bool Events { get; set; }

            [JsonProperty("lineups", NullValueHandling = NullValueHandling.Ignore)]
            public bool Lineups { get; set; }

            [JsonProperty("statistics_fixtures", NullValueHandling = NullValueHandling.Ignore)]
            public bool StatisticsFixtures { get; set; }

            [JsonProperty("statistics_players", NullValueHandling = NullValueHandling.Ignore)]
            public bool StatisticsPlayers { get; set; }
        }

        public class Coverage
        {
            [JsonProperty("fixtures", NullValueHandling = NullValueHandling.Ignore)]
            public Fixtures Fixtures { get; set; }

            [JsonProperty("standings", NullValueHandling = NullValueHandling.Ignore)]
            public bool Standings { get; set; }

            [JsonProperty("players", NullValueHandling = NullValueHandling.Ignore)]
            public bool Players { get; set; }

            [JsonProperty("top_scorers", NullValueHandling = NullValueHandling.Ignore)]
            public bool TopScorers { get; set; }

            [JsonProperty("top_assists", NullValueHandling = NullValueHandling.Ignore)]
            public bool TopAssists { get; set; }

            [JsonProperty("top_cards", NullValueHandling = NullValueHandling.Ignore)]
            public bool TopCards { get; set; }

            [JsonProperty("injuries", NullValueHandling = NullValueHandling.Ignore)]
            public bool Injuries { get; set; }

            [JsonProperty("predictions", NullValueHandling = NullValueHandling.Ignore)]
            public bool Predictions { get; set; }

            [JsonProperty("odds", NullValueHandling = NullValueHandling.Ignore)]
            public bool Odds { get; set; }
        }

        public class Season
        {
            [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
            public int Year { get; set; }

            [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
            public string Start { get; set; }

            [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
            public string End { get; set; }

            [JsonProperty("current", NullValueHandling = NullValueHandling.Ignore)]
            public bool Current { get; set; }

            [JsonProperty("coverage", NullValueHandling = NullValueHandling.Ignore)]
            public Coverage Coverage { get; set; }
        }

        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class Team
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public int Id { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("logo", NullValueHandling = NullValueHandling.Ignore)]
            public string Logo { get; set; }
        }

        public class Goals
        {
            [JsonProperty("for", NullValueHandling = NullValueHandling.Ignore)]
            public int For { get; set; }

            [JsonProperty("against", NullValueHandling = NullValueHandling.Ignore)]
            public int Against { get; set; }

            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int? Total { get; set; }

            [JsonProperty("conceded", NullValueHandling = NullValueHandling.Ignore)]
            public int? Conceded { get; set; }

            [JsonProperty("assists", NullValueHandling = NullValueHandling.Ignore)]
            public int? Assists { get; set; }

            [JsonProperty("saves", NullValueHandling = NullValueHandling.Ignore)]
            public int? Saves { get; set; }
        }

        public class All
        {
            [JsonProperty("played", NullValueHandling = NullValueHandling.Ignore)]
            public int Played { get; set; }

            [JsonProperty("win", NullValueHandling = NullValueHandling.Ignore)]
            public int Win { get; set; }

            [JsonProperty("draw", NullValueHandling = NullValueHandling.Ignore)]
            public int Draw { get; set; }

            [JsonProperty("lose", NullValueHandling = NullValueHandling.Ignore)]
            public int Lose { get; set; }

            [JsonProperty("goals", NullValueHandling = NullValueHandling.Ignore)]
            public Goals Goals { get; set; }
        }

        public class Home
        {
            [JsonProperty("played", NullValueHandling = NullValueHandling.Ignore)]
            public int Played { get; set; }

            [JsonProperty("win", NullValueHandling = NullValueHandling.Ignore)]
            public int Win { get; set; }

            [JsonProperty("draw", NullValueHandling = NullValueHandling.Ignore)]
            public int Draw { get; set; }

            [JsonProperty("lose", NullValueHandling = NullValueHandling.Ignore)]
            public int Lose { get; set; }

            [JsonProperty("goals", NullValueHandling = NullValueHandling.Ignore)]
            public Goals Goals { get; set; }
        }

        public class Away
        {
            [JsonProperty("played", NullValueHandling = NullValueHandling.Ignore)]
            public int Played { get; set; }

            [JsonProperty("win", NullValueHandling = NullValueHandling.Ignore)]
            public int Win { get; set; }

            [JsonProperty("draw", NullValueHandling = NullValueHandling.Ignore)]
            public int Draw { get; set; }

            [JsonProperty("lose", NullValueHandling = NullValueHandling.Ignore)]
            public int Lose { get; set; }

            [JsonProperty("goals", NullValueHandling = NullValueHandling.Ignore)]
            public Goals Goals { get; set; }
        }

        public class Standing
        {
            [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
            public int Rank { get; set; }

            [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
            public Team Team { get; set; }

            [JsonProperty("points", NullValueHandling = NullValueHandling.Ignore)]
            public int Points { get; set; }

            [JsonProperty("goalsDiff", NullValueHandling = NullValueHandling.Ignore)]
            public int GoalsDiff { get; set; }

            [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
            public string Group { get; set; }

            [JsonProperty("form", NullValueHandling = NullValueHandling.Ignore)]
            public string Form { get; set; }

            [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
            public string Status { get; set; }

            [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
            public string Description { get; set; }

            [JsonProperty("all", NullValueHandling = NullValueHandling.Ignore)]
            public All All { get; set; }

            [JsonProperty("home", NullValueHandling = NullValueHandling.Ignore)]
            public Home Home { get; set; }

            [JsonProperty("away", NullValueHandling = NullValueHandling.Ignore)]
            public Away Away { get; set; }

            [JsonProperty("update", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime Update { get; set; }
        }

        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
        public class Parameters
        {
            [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
            public string Team { get; set; }

            [JsonProperty("season", NullValueHandling = NullValueHandling.Ignore)]
            public string Season { get; set; }

            [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
            //, NullValueHandling = NullValueHandling.Ignore
            public string Page { get; set; }
        }

        public class Birth
        {
            [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
            public string Date { get; set; }

            [JsonProperty("place", NullValueHandling = NullValueHandling.Ignore)]
            public string Place { get; set; }

            [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
            public string Country { get; set; }
        }

        public class Player
        {
            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public int Id { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
            public string Firstname { get; set; }

            [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
            public string Lastname { get; set; }

            [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
            public int Age { get; set; }

            [JsonProperty("birth", NullValueHandling = NullValueHandling.Ignore)]
            public Birth Birth { get; set; }

            [JsonProperty("nationality", NullValueHandling = NullValueHandling.Ignore)]
            public string Nationality { get; set; }

            [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
            public string Height { get; set; }

            [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
            public string Weight { get; set; }

            [JsonProperty("injured", NullValueHandling = NullValueHandling.Ignore)]
            public bool Injured { get; set; }

            [JsonProperty("photo", NullValueHandling = NullValueHandling.Ignore)]
            public string Photo { get; set; }
        }


        public class Games
        {
            [JsonProperty("appearences", NullValueHandling = NullValueHandling.Ignore)]
            public int? Appearences { get; set; }

            [JsonProperty("lineups", NullValueHandling = NullValueHandling.Ignore)]
            public int? Lineups { get; set; }

            [JsonProperty("minutes", NullValueHandling = NullValueHandling.Ignore)]
            public int? Minutes { get; set; }

            [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
            public object Number { get; set; }

            [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
            public string Position { get; set; }

            [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
            public string Rating { get; set; }

            [JsonProperty("captain", NullValueHandling = NullValueHandling.Ignore)]
            public bool Captain { get; set; }
        }

        public class Substitutes
        {
            [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore)]
            public int? In { get; set; }

            [JsonProperty("out", NullValueHandling = NullValueHandling.Ignore)]
            public int? Out { get; set; }

            [JsonProperty("bench", NullValueHandling = NullValueHandling.Ignore)]
            public int? Bench { get; set; }
        }

        public class Shots
        {
            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int? Total { get; set; }

            [JsonProperty("on", NullValueHandling = NullValueHandling.Ignore)]
            public int? On { get; set; }
        }
        public class Passes
        {
            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int? Total { get; set; }

            [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
            public int? Key { get; set; }

            [JsonProperty("accuracy", NullValueHandling = NullValueHandling.Ignore)]
            public int? Accuracy { get; set; }
        }

        public class Tackles
        {
            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int? Total { get; set; }

            [JsonProperty("blocks", NullValueHandling = NullValueHandling.Ignore)]
            public int? Blocks { get; set; }

            [JsonProperty("interceptions", NullValueHandling = NullValueHandling.Ignore)]
            public int? Interceptions { get; set; }
        }

        public class Duels
        {
            [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
            public int? Total { get; set; }

            [JsonProperty("won", NullValueHandling = NullValueHandling.Ignore)]
            public int? Won { get; set; }
        }

        public class Dribbles
        {
            [JsonProperty("attempts", NullValueHandling = NullValueHandling.Ignore)]
            public int? Attempts { get; set; }

            [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
            public int? Success { get; set; }

            [JsonProperty("past", NullValueHandling = NullValueHandling.Ignore)]
            public object Past { get; set; }
        }

        public class Fouls
        {
            [JsonProperty("drawn", NullValueHandling = NullValueHandling.Ignore)]
            public int? Drawn { get; set; }

            [JsonProperty("committed", NullValueHandling = NullValueHandling.Ignore)]
            public int? Committed { get; set; }
        }

        public class Cards
        {
            [JsonProperty("yellow", NullValueHandling = NullValueHandling.Ignore)]
            public int? Yellow { get; set; }

            [JsonProperty("yellowred", NullValueHandling = NullValueHandling.Ignore)]
            public int? Yellowred { get; set; }

            [JsonProperty("red", NullValueHandling = NullValueHandling.Ignore)]
            public int? Red { get; set; }
        }

        public class Penalty
        {
            [JsonProperty("won", NullValueHandling = NullValueHandling.Ignore)]
            public object Won { get; set; }

            [JsonProperty("commited", NullValueHandling = NullValueHandling.Ignore)]
            public object Commited { get; set; }

            [JsonProperty("scored", NullValueHandling = NullValueHandling.Ignore)]
            public int? Scored { get; set; }

            [JsonProperty("missed", NullValueHandling = NullValueHandling.Ignore)]
            public int? Missed { get; set; }

            [JsonProperty("saved", NullValueHandling = NullValueHandling.Ignore)]
            public int? Saved { get; set; }
        }

        public class Statistic
        {
            [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
            public Team Team { get; set; }

            [JsonProperty("league", NullValueHandling = NullValueHandling.Ignore)]
            public League League { get; set; }

            [JsonProperty("games", NullValueHandling = NullValueHandling.Ignore)]
            public Games Games { get; set; }

            [JsonProperty("substitutes", NullValueHandling = NullValueHandling.Ignore)]
            public Substitutes Substitutes { get; set; }

            [JsonProperty("shots", NullValueHandling = NullValueHandling.Ignore)]
            public Shots Shots { get; set; }

            [JsonProperty("goals", NullValueHandling = NullValueHandling.Ignore)]
            public Goals Goals { get; set; }

            [JsonProperty("passes", NullValueHandling = NullValueHandling.Ignore)]
            public Passes Passes { get; set; }

            [JsonProperty("tackles", NullValueHandling = NullValueHandling.Ignore)]
            public Tackles Tackles { get; set; }

            [JsonProperty("duels", NullValueHandling = NullValueHandling.Ignore)]
            public Duels Duels { get; set; }

            [JsonProperty("dribbles", NullValueHandling = NullValueHandling.Ignore)]
            public Dribbles Dribbles { get; set; }

            [JsonProperty("fouls", NullValueHandling = NullValueHandling.Ignore)]
            public Fouls Fouls { get; set; }

            [JsonProperty("cards", NullValueHandling = NullValueHandling.Ignore)]
            public Cards Cards { get; set; }

            [JsonProperty("penalty", NullValueHandling = NullValueHandling.Ignore)]
            public Penalty Penalty { get; set; }
        }

    }
}
