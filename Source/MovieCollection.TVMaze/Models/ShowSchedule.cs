using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowSchedule
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("days")]
        public IEnumerable<string> Days { get; set; }
    }
}
