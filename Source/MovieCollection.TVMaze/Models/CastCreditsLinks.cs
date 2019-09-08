using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class CastCreditsLinks
    {
        [JsonProperty("show")]
        public LinkDefinition ShowLink { get; set; }

        [JsonProperty("character")]
        public LinkDefinition CharacterLink { get; set; }
    }
}