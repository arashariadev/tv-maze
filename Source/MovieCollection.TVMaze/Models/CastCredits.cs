using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class CastCredits
    {
        [JsonProperty("_links")]
        public CastCreditsLinks Links { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }
}