using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Schedule
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("number")]
        public long? Number { get; set; }

        [JsonProperty("airdate")]
        public DateTimeOffset AirDate { get; set; }

        [JsonProperty("airtime")]
        public string AirTime { get; set; }

        [JsonProperty("airstamp")]
        public DateTimeOffset AirStamp { get; set; }

        [JsonProperty("runtime")]
        public long? Runtime { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }
    }
}
