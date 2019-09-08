using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Alias
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }
    }
}