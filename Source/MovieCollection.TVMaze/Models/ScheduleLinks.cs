using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ScheduleLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}
