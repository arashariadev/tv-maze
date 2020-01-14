namespace MovieCollection.TVMaze
{
    public class TVMazeConfiguration : ITVMazeConfiguration
    {
        public TVMazeConfiguration() : base() => BaseAddress = "http://api.tvmaze.com";

        public TVMazeConfiguration(string apiKey) : this() => APIKey = apiKey;

        /// <summary>
        /// Gets or sets API's base address to bypass restrictions if necessary.
        /// </summary>
        public string BaseAddress { get; set; }

        public string APIKey { get; set; }
    }
}
