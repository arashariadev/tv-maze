using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class CrewCreditsLinks
    {
        [JsonProperty("show")]
        public LinkDefinition ShowLink { get; set; }
    }
}