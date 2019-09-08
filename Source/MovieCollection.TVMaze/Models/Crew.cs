using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Crew
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
