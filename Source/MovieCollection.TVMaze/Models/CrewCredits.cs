using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class CrewCredits
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("_links")]
        public CrewCreditsLinks Links { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }
}