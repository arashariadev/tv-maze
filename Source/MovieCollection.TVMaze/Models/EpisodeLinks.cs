using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class EpisodeLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}