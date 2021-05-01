using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Season
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("episodeOrder")]
        public int? EpisodeOrder { get; set; }

        [JsonProperty("premiereDate")]
        public DateTimeOffset? PremiereDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("network")]
        public Network Network { get; set; }

        [JsonProperty("webChannel")]
        public Network WebChannel { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}