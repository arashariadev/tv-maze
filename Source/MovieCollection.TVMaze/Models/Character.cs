using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Character
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("_links")]
        public CharacterLinks Links { get; set; }
    }
}