using System;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class LinkDefinition
    {
        [JsonProperty("href")]
        public Uri Uri { get; set; }
    }
}