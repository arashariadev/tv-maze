namespace MovieCollection.TVMaze
{
    /// <summary>
    /// The <c>TVMazeConfiguration</c> class.
    /// </summary>
    public class TVMazeConfiguration : ITVMazeConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeConfiguration"/> class.
        /// </summary>
        public TVMazeConfiguration()
            : base()
        {
            BaseAddress = "http://api.tvmaze.com";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeConfiguration"/> class.
        /// </summary>
        /// <param name="apiKey">the api key.</param>
        public TVMazeConfiguration(string apiKey)
            : this()
        {
            APIKey = apiKey;
        }

        /// <summary>
        /// Gets or sets base address to bypass restrictions if necessary.
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets api key.
        /// </summary>
        public string APIKey { get; set; }
    }
}
