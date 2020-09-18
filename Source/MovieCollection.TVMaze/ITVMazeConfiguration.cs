namespace MovieCollection.TVMaze
{
    public interface ITVMazeConfiguration
    {
        string APIKey { get; set; }

        string BaseAddress { get; set; }
    }
}