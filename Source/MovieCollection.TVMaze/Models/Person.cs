using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Person
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("deathday")]
        public string Deathday { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}