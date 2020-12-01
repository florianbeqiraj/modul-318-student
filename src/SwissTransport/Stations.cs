using System.Collections.Generic;
using Newtonsoft.Json;

namespace SwissTransport
{
    public class Coordinate
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("x")]
        public double XCoordinate { get; set; }

        [JsonProperty("y")]
        public double YCoordinate { get; set; }
    }

    public class Station
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public int? Score { get; set; }

        [JsonProperty("coordinate")]
        public Coordinate Coordinate { get; set; }

        [JsonProperty("distance")]
        public double? Distance { get; set; }
    }

    public class Stations
    {
        [JsonProperty("stations")]
        public List<Station> StationList { get; set; }
    }

    public class Section
    {
        [JsonProperty("journey")]
        public Journey Journey { get; set; }
    }

    public class Journey
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("passList")]
        public List<Pass> Passes { get; set; }
    }

    public class Pass
    {
        [JsonProperty("station")]
        public Station Station { get; set; }

        [JsonProperty("arrival")]
        public string Arrival { get; set; }

        [JsonProperty("departure")]
        public string Departure { get; set; }
    }
}