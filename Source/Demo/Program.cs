﻿using System;
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
            // Note: apiKey is optional.
            _service = new TVMazeService(_httpClient);

Start:
            Console.Clear();
            Console.WriteLine("Welcome to TVMaze demo.\n");

            string[] items = new string[]
            {
                "1. Search TV Shows",
                "2. Search Single TV Show",
                "3. Search By IMDb Id",
                "4. Search By TVDb Id",
                "5. Search By TVRage Id",
                "6. Search People",
                "7. Get Today's Schedule",
                "8. Get Show Info (by Id)",
                "9. Get Show Episodes",
                "10. Get Show Specific Episode",
                "11. Get Show Episodes by date",
                "12. Get Show Seasons",
                "13. Get Season Episodes",
                "14. Get Show Cast",
                "15. Get Show Crew",
                "16. Get Show AKA's",
                "17. Get Episode Info",
                "18. Get Person Info",
                "19. Get Person Cast Credits",
                "20. Get Crew Credits"
            };

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.Write("\nPlease select an option: ");
            int input = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            switch (input)
            {
                default:
                case 1:
                    await SearchShows();
                    break;
                case 2:
                    await SearchSingleShow();
                    break;
                case 3:
                    await SearchByIMDbId();
                    break;
                case 4:
                    await SearchByTVDbId();
                    break;
                case 5:
                    await SearchByTVRageId();
                    break;
                case 6:
                    await SearchPeople();
                    break;
                case 7:
                    await GetSchedule();
                    break;
                case 8:
                    await GetShowInfo();
                    break;
                case 9:
                    await GetShowEpisodes();
                    break;
                case 10:
                    await GetShowEpisode();
                    break;
                case 11:
                    await GetShowEpisodesByDate();
                    break;
                case 12:
                    await GetShowSeasons();
                    break;
                case 13:
                    await GetSeasonEpisodes();
                    break;
                case 14:
                    await GetShowCast();
                    break;
                case 15:
                    await GetShowCrew();
                    break;
                case 16:
                    await GetShowAliases();
                    break;
                case 17:
                    await GetEpisodeInfo();
                    break;
                case 18:
                    await GetPersonInfo();
                    break;
                case 19:
                    await GetCastCredits();
                    break;
                case 20:
                    await GetCrewCredits();
                    break;
            }

            // Wait for user to Exit
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey();

            goto Start;
        }

        private static async Task SearchShows()
        {
            var results = await _service.SearchShowsAsync("marvel");

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
            var item = await _service.SearchSingleShowAsync("frasier");

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task SearchByIMDbId()
        {
            var item = await _service.SearchByIMDbIdAsync("tt0098904");

            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Network Name: {0}", item.Network?.Name);
            Console.WriteLine("Summary: {0}", item.Summary);
        }

        private static async Task SearchByTVDbId()
        {
            var item = await _service.SearchByTVDbIdAsync("81189");

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
