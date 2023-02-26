using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace PIL_Fantasy_Data_Integration.API.Fantasy_Data.CommonDefinitions.FootBallResponses
{
    public partial class PlayerRoot
    {
        [JsonProperty("get", NullValueHandling = NullValueHandling.Ignore)]
        public string Get { get; set; }

        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public Parameters Parameters { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Errors { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public long? Results { get; set; }

        [JsonProperty("paging", NullValueHandling = NullValueHandling.Ignore)]
        public Paging Paging { get; set; }

        [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
        public List<Response> Response { get; set; }
    }

    public partial class Paging
    {
        [JsonProperty("current", NullValueHandling = NullValueHandling.Ignore)]
        public long? Current { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }
    }

    public partial class Parameters
    {
        [JsonProperty("fixture", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Fixture { get; set; }

        [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Team { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
        public Team Team { get; set; }

        [JsonProperty("players", NullValueHandling = NullValueHandling.Ignore)]
        public List<PlayerElement> Players { get; set; }
    }

    public partial class PlayerElement
    {
        [JsonProperty("player", NullValueHandling = NullValueHandling.Ignore)]
        public PlayerPlayer Player { get; set; }

        [JsonProperty("statistics", NullValueHandling = NullValueHandling.Ignore)]
        public List<Statistic> Statistics { get; set; }
    }

    public partial class PlayerPlayer
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("photo", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Photo { get; set; }
    }

    public partial class Statistic
    {
        [JsonProperty("games", NullValueHandling = NullValueHandling.Ignore)]
        public Games Games { get; set; }

        [JsonProperty("offsides", NullValueHandling = NullValueHandling.Ignore)]
        public long? Offsides { get; set; }

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

    public partial class Cards
    {
        [JsonProperty("yellow", NullValueHandling = NullValueHandling.Ignore)]
        public long? Yellow { get; set; }

        [JsonProperty("red", NullValueHandling = NullValueHandling.Ignore)]
        public long? Red { get; set; }
    }

    public partial class Dribbles
    {
        [JsonProperty("attempts", NullValueHandling = NullValueHandling.Ignore)]
        public long? Attempts { get; set; }

        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public long? Success { get; set; }

        [JsonProperty("past", NullValueHandling = NullValueHandling.Ignore)]
        public long? Past { get; set; }
    }

    public partial class Duels
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("won", NullValueHandling = NullValueHandling.Ignore)]
        public long? Won { get; set; }
    }

    public partial class Fouls
    {
        [JsonProperty("drawn", NullValueHandling = NullValueHandling.Ignore)]
        public long? Drawn { get; set; }

        [JsonProperty("committed", NullValueHandling = NullValueHandling.Ignore)]
        public long? Committed { get; set; }
    }

    public partial class Games
    {
        [JsonProperty("minutes", NullValueHandling = NullValueHandling.Ignore)]
        public long? Minutes { get; set; }

        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public long? Number { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public Position? Position { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public string Rating { get; set; }

        [JsonProperty("captain", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Captain { get; set; }

        [JsonProperty("substitute", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Substitute { get; set; }
    }

    public partial class Goals
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("conceded", NullValueHandling = NullValueHandling.Ignore)]
        public long? Conceded { get; set; }

        [JsonProperty("assists", NullValueHandling = NullValueHandling.Ignore)]
        public long? Assists { get; set; }

        [JsonProperty("saves", NullValueHandling = NullValueHandling.Ignore)]
        public long? Saves { get; set; }
    }

    public partial class Passes
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public long? Key { get; set; }

        [JsonProperty("accuracy", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Accuracy { get; set; }
    }

    public partial class Penalty
    {
        [JsonProperty("won", NullValueHandling = NullValueHandling.Ignore)]
        public long? Won { get; set; }

        [JsonProperty("commited", NullValueHandling = NullValueHandling.Ignore)]
        public long? Commited { get; set; }

        [JsonProperty("scored", NullValueHandling = NullValueHandling.Ignore)]
        public long? Scored { get; set; }

        [JsonProperty("missed", NullValueHandling = NullValueHandling.Ignore)]
        public long? Missed { get; set; }

        [JsonProperty("saved", NullValueHandling = NullValueHandling.Ignore)]
        public long? Saved { get; set; }
    }

    public partial class Shots
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("on", NullValueHandling = NullValueHandling.Ignore)]
        public long? On { get; set; }
    }

    public partial class Tackles
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        [JsonProperty("blocks", NullValueHandling = NullValueHandling.Ignore)]
        public long? Blocks { get; set; }

        [JsonProperty("interceptions", NullValueHandling = NullValueHandling.Ignore)]
        public long? Interceptions { get; set; }
    }

    public partial class Team
    {
        [JsonProperty("update", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Update { get; set; }
    }

    public enum Position { D, F, G, M };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                PositionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class PositionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Position) || t == typeof(Position?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "D":
                    return Position.D;
                case "F":
                    return Position.F;
                case "G":
                    return Position.G;
                case "M":
                    return Position.M;
            }
            throw new Exception("Cannot unmarshal type Position");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Position)untypedValue;
            switch (value)
            {
                case Position.D:
                    serializer.Serialize(writer, "D");
                    return;
                case Position.F:
                    serializer.Serialize(writer, "F");
                    return;
                case Position.G:
                    serializer.Serialize(writer, "G");
                    return;
                case Position.M:
                    serializer.Serialize(writer, "M");
                    return;
            }
            throw new Exception("Cannot marshal type Position");
        }

        public static readonly PositionConverter Singleton = new PositionConverter();
    }
}
