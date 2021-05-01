using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Episode
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("season")]
        public int Season { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("airdate")]
        public string AirDate { get; set; }

        [JsonProperty("airtime")]
        public string AirTime { get; set; }

        [JsonProperty("airstamp")]
        public DateTimeOffset? AirStamp { get; set; }

        [JsonProperty("runtime")]
        public int Runtime { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}