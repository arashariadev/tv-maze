using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieCollection.TVMaze.Models;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze
{
    public class TVMazeService : ITVMazeService
    {
        private readonly HttpClient _httpClient;
        private readonly ITVMazeConfiguration _configuration;

        public TVMazeService(HttpClient httpClient)
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = new TVMazeConfiguration();
        }

        public TVMazeService(HttpClient httpClient, ITVMazeConfiguration configuration)
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private static string GetParametersString(IEnumerable<UrlParameter> parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in parameters)
            {
                builder.Append(builder.Length == 0 ? "?" : "&");
                builder.Append(item.ToString());
            }
            return builder.ToString();
        }

        private async Task<string> GetJsonAsync(string requestUrl, IEnumerable<UrlParameter> requestParameters = null)
        {
            string url = _configuration.BaseAddress + requestUrl;

            var parameters = new List<UrlParameter>();

            // Add api key if defined to list
            if (!string.IsNullOrWhiteSpace(_configuration.APIKey))
            {
                parameters.Add(new UrlParameter("apikey", _configuration.APIKey));
            }

            // Add request specific parameters to list
            if (requestParameters != null)
            {
                parameters.AddRange(requestParameters);
            }

            // Concat parameters to URL
            url += GetParametersString(parameters);

            using var response = await _httpClient.GetAsync(new Uri(url))
                .ConfigureAwait(false);

            // TODO: Maybe handle API Rate limit (429)?
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        private static List<UrlParameter> GetEmbeddedPrameters(string[] embed)
        {
            if (embed is null)
            {
                throw new ArgumentNullException(nameof(embed));
            }

            var parameters = new List<UrlParameter>();

            if (embed.Length == 1)
            {
                parameters.Add(new UrlParameter("embed", embed[0]));
            }
            else if (embed.Length > 1)
            {
                foreach (var item in embed)
                {
                    parameters.Add(new UrlParameter("embed[]", item));
                }
            }

            return parameters;
        }

        public async Task<IList<Search>> SearchShowsAsync(string query)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("q", System.Web.HttpUtility.UrlEncode(query))
            };

            string json = await GetJsonAsync("/search/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Search>>(json);
        }

        public async Task<Show> SearchSingleShowAsync(string query, params string[] embed)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("q", System.Web.HttpUtility.UrlEncode(query))
            };

            if (embed != null && embed.Length != 0)
            {
                parameters.AddRange(GetEmbeddedPrameters(embed));
            }

            string json = await GetJsonAsync("/singlesearch/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        public async Task<Show> SearchByIMDbIdAsync(string imdbId)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("imdb", imdbId)
            };

            string json = await GetJsonAsync("/lookup/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        public async Task<Show> SearchByTVDbIdAsync(string tvdbId)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("thetvdb", tvdbId)
            };

            string json = await GetJsonAsync("/lookup/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        public async Task<Show> SearchByTVRageIdAsync(string tvRageId)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("tvrage", tvRageId)
            };

            string json = await GetJsonAsync("/lookup/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        public async Task<IList<SearchPerson>> SearchPeopleAsync(string query)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("q", System.Web.HttpUtility.UrlEncode(query))
            };

            string json = await GetJsonAsync("/search/people", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<SearchPerson>>(json);
        }

        /// <summary>
        /// The schedule is a complete list of episodes that air in a given country on a given date.
        /// Episodes are returned in the order in which they are aired, and full information about the episode and the corresponding show is included.
        /// Note that contrary to what you might expect, the ISO country code for the United Kingdom is not UK, but GB.
        /// </summary>
        /// <param name="dateTime">Defaults to the current day.</param>
        /// <param name="country">Defaults to US</param>
        /// <returns></returns>
        public async Task<IList<Schedule>> GetScheduleAsync(DateTime? dateTime = null, string country = null)
        {
            // Date is an ISO 8601 formatted date; defaults to the current day.
            // CountryCode is an ISO 3166-1 code of the country; defaults to US 

            var parameters = new List<UrlParameter>();

            if (dateTime.HasValue)
            {
                parameters.Add(new UrlParameter("date", dateTime.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(country))
            {
                parameters.Add(new UrlParameter("country", country));
            }

            string json = await GetJsonAsync("/schedule", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Schedule>>(json);
        }

        /// <summary>
        ///  The full schedule is a list of all future episodes known to TVmaze, regardless of their country.
        ///  Be advised that this endpoint's response is at least several MB large.
        ///  As opposed to the other endpoints, results are cached for 24 hours. 
        /// </summary>
        public async Task<IList<Schedule>> GetFullScheduleAsync()
        {
            string json = await GetJsonAsync("/schedule/full")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Schedule>>(json);
        }

        /// <summary>
        /// Retrieve all primary information for a given show.
        /// This endpoint allows embedding of additional information.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public async Task<Show> GetShowInfoAsync(int id, params string[] embed)
        {
            var parameters = new List<UrlParameter>();

            if (embed != null && embed.Length != 0)
            {
                parameters.AddRange(GetEmbeddedPrameters(embed));
            }

            string json = await GetJsonAsync($"/shows/{id}", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        /// <summary>
        ///  A complete list of episodes for the given show.
        ///  Episodes are returned in their airing order, and include full episode information.
        ///  By default, specials are not included in the list. 
        /// </summary>
        public async Task<IList<Episode>> GetShowEpisodesListAsync(int showId, bool specials = false)
        {
            var parameters = new List<UrlParameter>();

            if (specials == true)
            {
                parameters.Add(new UrlParameter("specials", "1"));
            }

            string json = await GetJsonAsync($"/shows/{showId}/episodes", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Episode>>(json);
        }

        /// <summary>
        ///  Retrieve one specific episode from this show given its season number and episode number.
        ///  This either returns the full information for one episode, or a HTTP 404. 
        /// </summary>
        public async Task<Episode> GetShowEpisodeAsync(int showId, int season, int episode)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("season", season),
                new UrlParameter("number", episode),
            };

            string json = await GetJsonAsync($"/shows/{showId}/episodebynumber", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Episode>(json);
        }

        /// <summary>
        ///  Retrieve all episodes from this show that have aired on a specific date.
        ///  This either returns an array of full episode info, or a HTTP 404.
        ///  Useful for daily (talk) shows that don't adhere to a common season numbering. 
        /// </summary>
        public async Task<IList<Episode>> GetShowEpisodesByDateAsync(int showId, DateTime dateTime)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("date", dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            };

            string json = await GetJsonAsync($"/shows/{showId}/episodesbydate", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Episode>>(json);
        }

        /// <summary>
        ///  A complete list of seasons for the given show.
        ///  Seasons are returned in ascending order and contain the full information that's known about them. 
        /// </summary>
        public async Task<IList<Season>> GetShowSeasonsAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/seasons")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Season>>(json);
        }

        /// <summary>
        ///    A list of episodes in this season. Specials are always included in this list;
        ///    they can be recognized by a NULL value for number. 
        /// </summary>
        public async Task<IList<Episode>> GetSeasonEpisodesAsync(int seasonId)
        {
            string json = await GetJsonAsync($"/seasons/{seasonId}/episodes")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Episode>>(json);
        }

        /// <summary>
        /// A list of main cast for a show.
        /// Each cast item is a combination of a person and a character.
        /// Items are ordered by importance, which is determined by the total number of appearances of the given character in this show. 
        /// </summary>
        public async Task<IList<Cast>> GetShowCastAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/cast")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Cast>>(json);
        }

        /// <summary>
        ///  A list of main crew for a show.
        ///  Each crew item is a combination of a person and their crew type. 
        /// </summary>
        public async Task<IList<Crew>> GetShowCrewAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/crew")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Crew>>(json);
        }

        /// <summary>
        ///  A list of AKA's (aliases) for a show. An AKA with its country set to null indicates an AKA in the show's original country.
        ///  Otherwise, it's the AKA for that show in the given foreign country. 
        /// </summary>
        public async Task<IList<ShowAlias>> GetShowAliasesAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/akas")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<ShowAlias>>(json);
        }

        /// <summary>
        /// A list of all shows in our database, with all primary information included.
        /// You can use this endpoint for example if you want to build a local cache of all shows contained in the TVmaze database.
        /// This endpoint is paginated, with a maximum of 250 results per page.
        /// The pagination is based on show ID, e.g. page 0 will contain shows with IDs between 0 and 250.
        /// This means a single page might contain less than 250 results, in case of deletions, but it also guarantees that deletions won't cause shuffling in the page numbering for other shows.
        /// Because of this, you can implement a daily/weekly sync simply by starting at the page number where you last left off, and be sure you won't skip over any entries.
        /// For example, if the last show in your local cache has an ID of 1800, you would start the re-sync at page number floor(1800/250) = 7.
        /// After this, simply increment the page number by 1 until you receive a HTTP 404 response code, which indicates that you've reached the end of the list.
        /// As opposed to the other endpoints, results from the show index are cached for up to 24 hours.
        /// </summary>
        public async Task<IList<Show>> GetShowIndexAsync(int page = 1)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("page", page)
            };

            string json = await GetJsonAsync("/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Show>>(json);
        }

        /// <summary>
        ///  Retrieve all primary information for a given episode.
        ///  This endpoint allows embedding of additional information.
        /// </summary>
        public async Task<Episode> GetEpisodeByIdAsync(int episodeId, params string[] embed)
        {
            var parameters = new List<UrlParameter>();

            if (embed != null && embed.Length != 0)
            {
                parameters.AddRange(GetEmbeddedPrameters(embed));
            }

            string json = await GetJsonAsync($"/episodes/{episodeId}", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Episode>(json);
        }

        /// <summary>
        ///  Retrieve all primary information for a given person.
        ///  This endpoint allows embedding of additional information.
        /// </summary>
        public async Task<Person> GetPersonInfoAsync(int personId, params string[] embed)
        {
            var parameters = new List<UrlParameter>();

            if (embed != null && embed.Length != 0)
            {
                parameters.AddRange(GetEmbeddedPrameters(embed));
            }

            string json = await GetJsonAsync($"/people/{personId}", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Person>(json);
        }

        /// <summary>
        ///  Retrieve all (show-level) cast credits for a person.
        ///  A cast credit is a combination of both a show and a character.
        ///  By default, only a reference to each show and character will be returned.
        ///  However, this endpoint supports embedding, which means full information for the shows and characters can be included. 
        /// </summary>
        public async Task<IList<CastCredits>> GetCastCreditsAsync(int personId, params string[] embed)
        {
            var parameters = new List<UrlParameter>();

            if (embed != null && embed.Length != 0)
            {
                parameters.AddRange(GetEmbeddedPrameters(embed));
            }

            string json = await GetJsonAsync($"/people/{personId}/castcredits", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<CastCredits>>(json);
        }

        /// <summary>
        ///  Retrieve all (show-level) crew credits for a person.
        ///  A crew credit is combination of both a show and a crew type.
        ///  By default, only a reference to each show will be returned.
        ///  However, this endpoint supports embedding, which means full information for the shows can be included. 
        /// </summary>
        public async Task<IList<CrewCredits>> GetCrewCreditsAsync(int personId, params string[] embed)
        {
            var parameters = new List<UrlParameter>();

            if (embed != null && embed.Length != 0)
            {
                parameters.AddRange(GetEmbeddedPrameters(embed));
            }

            string json = await GetJsonAsync($"/people/{personId}/crewcredits", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<CrewCredits>>(json);
        }
    }
}
