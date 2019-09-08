using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
    }
}
