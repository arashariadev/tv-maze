using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Rating
    {
        [JsonProperty("average")]
        public double? Average { get; set; }
    }
}
