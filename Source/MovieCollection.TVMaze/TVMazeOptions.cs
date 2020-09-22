namespace MovieCollection.TVMaze
{
    /// <summary>
    /// The <c>TVMazeOptions</c> class.
    /// </summary>
    public class TVMazeOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeOptions"/> class.
        /// </summary>
        public TVMazeOptions()
            : base()
        {
            ApiAddress = "http://api.tvmaze.com";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeOptions"/> class.
        /// </summary>
        /// <param name="apiKey">the api key.</param>
        public TVMazeOptions(string apiKey)
            : this()
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Gets or sets base address to bypass restrictions if necessary.
        /// </summary>
        public string ApiAddress { get; set; }

        /// <summary>
        /// Gets or sets apiKey.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
