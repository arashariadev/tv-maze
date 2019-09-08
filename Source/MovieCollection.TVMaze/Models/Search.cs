using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Search
    {
        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }
    }
}
