using System;

namespace DemoSimple
{
    class Program
    {
        static void Main()
        {
            // SearchShow();
            GetSchedule();

            // Wait for user to exit
            Console.ReadKey();
        }

        private static async void SearchShow()
        {
            var service = new MovieCollection.TVMaze.Service();
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

        private static async void GetSchedule()
        {
            var service = new MovieCollection.TVMaze.Service();
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

        private static async void GetShowEpisode()
        {
            var service = new MovieCollection.TVMaze.Service();
            var item = await service.GetShowEpisodeAsync(16149, 2, 3);

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine("******************************");
        }
    }
}
