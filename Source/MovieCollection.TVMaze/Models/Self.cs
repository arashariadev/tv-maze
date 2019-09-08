using Newtonsoft.Json;
using System;

namespace MovieCollection.TVMaze.Models
{
    public class LinkDefinition
    {
        [JsonProperty("href")]
        public Uri Uri { get; set; }
    }
}