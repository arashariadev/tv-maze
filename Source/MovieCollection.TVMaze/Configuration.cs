namespace MovieCollection.TVMaze
{
    public class Configuration : IConfiguration
    {
        public Configuration() : base() { }
        public Configuration(string apiKey) : base() => APIKey = apiKey;

        /// <summary>
        /// Gets or sets API's base address to bypass restrictions if necessary.
        /// </summary>
        public string BaseAddress { get; set; } = "http://api.tvmaze.com";

        public string APIKey { get; set; }
    }
}
