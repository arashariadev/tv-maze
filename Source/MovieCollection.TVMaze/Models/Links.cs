using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Links
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}