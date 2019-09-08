using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class SeasonLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}