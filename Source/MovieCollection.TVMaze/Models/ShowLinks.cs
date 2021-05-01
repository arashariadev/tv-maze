using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowLinks : Links
    {
        [JsonProperty("previousepisode")]
        public LinkDefinition PreviousEpisode { get; set; }

        [JsonProperty("nextepisode")]
        public LinkDefinition Nextepisode { get; set; }
    }
}
