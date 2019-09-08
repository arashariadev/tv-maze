using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class PersonLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}