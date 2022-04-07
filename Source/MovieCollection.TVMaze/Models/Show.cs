using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MovieCollection.TVMaze.Models
{
    public class Show
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<string> Genres { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("runtime")]
        public int? Runtime { get; set; }

        [JsonProperty("averageRuntime")]
        public int? AverageRuntime { get; set; }

        [JsonProperty("premiered")]
        public DateTimeOffset? Premiered { get; set; }

        [JsonProperty("ended")]
        public DateTimeOffset? Ended { get; set; }

        [JsonProperty("officialSite")]
        public Uri OfficialSite { get; set; }

        [JsonProperty("schedule")]
        public ShowSchedule Schedule { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("network")]
        public Network Network { get; set; }

        [JsonProperty("webChannel")]
        public Network WebChannel { get; set; }

        [JsonProperty("dvdCountry")]
        public Country DvdCountry { get; set; }

        [JsonProperty("externals")]
        public Externals Externals { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("updated")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset? Updated { get; set; }

        [JsonProperty("_links")]
        public ShowLinks Links { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }
}
