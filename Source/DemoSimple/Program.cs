using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoSimple
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        // See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
        private static readonly HttpClient _httpClient = new HttpClient();

        private static async Task Main()
        {
            await GetSchedule();

            Console.ReadKey();
            Console.Clear();

            await SearchShow();

            Console.ReadKey();
            Console.Clear();

            await GetShowEpisode();

            // Wait for user to exit
            Console.ReadKey();
        }

        private static async Task SearchShow()
        {
            var service = new MovieCollection.TVMaze.TVMazeService(_httpClient);
            var results = await service.SearchShowsAsync("marvel");

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Show.Id);
                Console.WriteLine("Name: {0}", item.Show.Name);
                Console.WriteLine("Network Name: {0}", item.Show.Network?.Name);
                Console.WriteLine("Summary: {0}", item.Show.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetSchedule()
        {
            var service = new MovieCollection.TVMaze.TVMazeService(_httpClient);
            var results = await service.GetScheduleAsync(country: "GB");

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Show Name: {0}", item.Show?.Name);
                Console.WriteLine("Episode Name: {0}", item.Name);
                Console.WriteLine("Network Name: {0}", item.Show?.Network?.Name);
                Console.WriteLine("Summary: {0}", item.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowEpisode()
        {
            var service = new MovieCollection.TVMaze.TVMazeService(_httpClient);
            var item = await service.GetShowEpisodeAsync(16149, 2, 3);

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine("******************************");
        }
    }
}
