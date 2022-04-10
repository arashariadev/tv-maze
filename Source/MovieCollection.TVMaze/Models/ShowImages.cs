using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowImages
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("main")]
        public bool Main { get; set; }

        [JsonProperty("resolutions")]
        public ShowImageResolutions Resolutions { get; set; }
    }
}
