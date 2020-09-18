using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Image
    {
        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }
    }
}