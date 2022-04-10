using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowImageResolutions
    {
        [JsonProperty("original")]
        public ShowImage Original { get; set; }

        [JsonProperty("medium")]
        public ShowImage Medium { get; set; }
    }
}
