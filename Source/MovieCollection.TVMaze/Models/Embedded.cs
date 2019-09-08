using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Embedded
    {
        [JsonProperty("episodes")]
        public Episode[] Episodes { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }

        [JsonProperty("character")]
        public Show Character { get; set; }
    }
}
