using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class SearchPerson
    {
        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}