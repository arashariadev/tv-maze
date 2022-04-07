using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieCollection.TVMaze;

namespace Demo
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        // See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
        private static readonly HttpClient _httpClient = new HttpClient();
        private static TVMazeService _service;

        private static async Task Main()
        {
            // Initialize
            // Note: The 'ApiKey' is optional.
            _service = new TVMazeService(_httpClient);

Start:
            Console.Clear();
            Console.WriteLine("Welcome to the TVMaze demo.\n");

            string[] items = new string[]
            {
                "1. Show search",
                "2. Show single search",
                "3. Show search: IMDb Id",
                "4. Show search: TVDb Id",
                "5. Show search: TVRage Id",
                "6. People search",
                "7. Schedule",
                "8. Web/streaming schedule",
                "9. Show main information",
                "10. Show episode list",
                "11. Episode by number",
                "12. Episodes by date",
                "13. Show seasons",
                "14. Season episodes",
                "15. Show cast",
                "16. Show crew",
                "17. Show AKA's (aliases)",
                "18. Episode main information",
                "19. Person main information",
                "20. Person cast credits",
                "21. Person crew credits"
            };

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.Write("\nPlease select an option: ");
            int input = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            var task = input switch
            {
                2 => SearchSingleShow(),
                3 => SearchByIMDbId(),
                4 => SearchByTVDbId(),
                5 => SearchByTVRageId(),
                6 => SearchPeople(),
                7 => GetSchedule(),
                8 => GetWebSchedule(),
                9 => GetShowInfo(),
                10 => GetShowEpisodes(),
                11 => GetShowEpisode(),
                12 => GetShowEpisodesByDate(),
                13 => GetShowSeasons(),
                14 => GetSeasonEpisodes(),
                15 => GetShowCast(),
                16 => GetShowCrew(),
                17 => GetShowAliases(),
                18 => GetEpisodeInfo(),
                19 => GetPersonInfo(),
                20 => GetCastCredits(),
                21 => GetCrewCredits(),
                _ => SearchShows(),
            };

            await task;

            // Wait for user to Exit
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey();

            goto Start;
        }

        private static async Task SearchShows()
        {
            var results = await _service.SearchShowsAsync("fleabag");

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Show.Id);
                Console.WriteLine("Name: {0}", item.Show.Name);
                Console.WriteLine("Network Name: {0}", item.Show.Network?.Name);
                Console.WriteLine("Summary: {0}", item.Show.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task SearchSingleShow()
        {
            var item = await _service.SearchSingleShowAsync("black books");

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task SearchByIMDbId()
        {
            // BoJack Horseman
            var item = await _service.SearchByIMDbIdAsync("tt3398228");

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task SearchByTVDbId()
        {
            // Solar Opposites
            var item = await _service.SearchByTVDbIdAsync("375892");

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task SearchByTVRageId()
        {
            var item = await _service.SearchByTVRageIdAsync("24493");

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task SearchPeople()
        {
            var results = await _service.SearchPeopleAsync("cate");

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Person.Id);
                Console.WriteLine("Name: {0}", item.Person.Name);
                Console.WriteLine("Image: {0}", item.Person.Image?.Original);
                Console.WriteLine("Birthday: {0}", item.Person.Birthday);
                Console.WriteLine("Updated: {0}", item.Person.Updated);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetSchedule()
        {
            // Common mistake: if you want UK's schedule pass "GB" as country.
            var results = await _service.GetScheduleAsync(country: "US");

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

        private static async Task GetWebSchedule()
        {
            var results = await _service.GetStreamingScheduleAsync(country: "US");

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Show Name: {0}", item.Embedded?.Show?.Name);
                Console.WriteLine("Episode Name: {0}", item.Name);
                Console.WriteLine("Network Name: {0}", item.Embedded?.Show?.WebChannel?.Name);
                Console.WriteLine("Summary: {0}", item.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowInfo()
        {
            var item = await _service.GetShowInfoAsync(2);

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task GetShowEpisodes()
        {
            var results = await _service.GetShowEpisodesListAsync(16149);

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Summary: {0}", item.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowEpisode()
        {
            var item = await _service.GetShowEpisodeAsync(16149, 2, 3);

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
            Console.WriteLine("******************************");
        }

        private static async Task GetShowEpisodesByDate()
        {
            var results = await _service.GetShowEpisodesByDateAsync(17078, new DateTime(2019, 04, 12));

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Summary: {0}", item.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowSeasons()
        {
            var results = await _service.GetShowSeasonsAsync(17078);

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Number: {0}", item.Number);
                Console.WriteLine("PremiereDate: {0}", item.PremiereDate);
                Console.WriteLine("Summary: {0}", item.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetSeasonEpisodes()
        {
            var results = await _service.GetSeasonEpisodesAsync(82424);

            foreach (var item in results)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Number: {0}", item.Number);
                Console.WriteLine("Summary: {0}", item.Summary);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowCast()
        {
            var results = await _service.GetShowCastAsync(530);

            foreach (var item in results)
            {
                Console.WriteLine("Person Id: {0}", item.Person.Id);
                Console.WriteLine("Person Name: {0}", item.Person?.Name);
                Console.WriteLine("Person Image: {0}", item.Person?.Image?.Original);
                Console.WriteLine("Character Name: {0}", item.Character?.Name);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowCrew()
        {
            var results = await _service.GetShowCrewAsync(530);

            foreach (var item in results)
            {
                Console.WriteLine("Type: {0}", item.Type);
                Console.WriteLine("Person Id: {0}", item.Person.Id);
                Console.WriteLine("Person Name: {0}", item.Person?.Name);
                Console.WriteLine("Person Image: {0}", item.Person?.Image?.Original);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetShowAliases()
        {
            var results = await _service.GetShowAliasesAsync(171);

            foreach (var item in results)
            {
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Country: {0}", item.Country?.Name);
            }
        }

        private static async Task GetEpisodeInfo()
        {
            var item = await _service.GetEpisodeByIdAsync(48221);

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Season: {0}", item.Season);
            Console.WriteLine("Number: {0}", item.Number);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task GetPersonInfo()
        {
            var item = await _service.GetPersonInfoAsync(37759);

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Birthday: {0}", item.Birthday);
            Console.WriteLine("Image: {0}", item.Image?.Original);
            Console.WriteLine("Updated: {0}", item.Updated);
        }

        private static async Task GetCastCredits()
        {
            var results = await _service.GetCastCreditsAsync(40598, "show", "character");

            foreach (var item in results)
            {
                Console.WriteLine("Show Name: {0}", item.Embedded?.Show?.Name);
                Console.WriteLine("Character Name: {0}", item.Embedded?.Character?.Name);
                Console.WriteLine("******************************");
            }
        }

        private static async Task GetCrewCredits()
        {
            var results = await _service.GetCrewCreditsAsync(100, "show");

            foreach (var item in results)
            {
                Console.WriteLine("Type: {0}", item.Type);
                Console.WriteLine("Show Name: {0}", item.Embedded?.Show?.Name);
                Console.WriteLine("******************************");
            }
        }
    }
}
