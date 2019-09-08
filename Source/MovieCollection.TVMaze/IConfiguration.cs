namespace MovieCollection.TVMaze
{
    public interface IConfiguration
    {
        string APIKey { get; set; }
        string BaseAddress { get; set; }
    }
}