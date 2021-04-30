using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowLinks : Links
    {
        [JsonProperty("previousepisode")]
        public LinkDefinition PreviousEpisode { get; set; }
    }
}
