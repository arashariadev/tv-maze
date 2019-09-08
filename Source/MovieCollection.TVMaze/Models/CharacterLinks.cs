using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class CharacterLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}