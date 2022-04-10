using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class ShowImage
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
