using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowSchedule
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("days")]
        public string[] Days { get; set; }
    }
}
