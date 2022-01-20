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
    /// <summary>
    /// The <c>TVMazeService</c> class.
    /// </summary>
    public class TVMazeService
    {
        private readonly HttpClient _httpClient;
        private readonly TVMazeOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        public TVMazeService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = new TVMazeOptions();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="options">An instance of <see cref="TVMazeOptions"/>.</param>
        public TVMazeService(HttpClient httpClient, TVMazeOptions options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Search through all the shows in our database by the show's name.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Search>> SearchShowsAsync(string query)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            return await GetJsonAsync<IList<Search>>("/search/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Search for a single tv show.
        /// </summary>
        /// <remarks>
        /// This endpoint allows embedding of additional information.
        /// </remarks>
        /// <param name="query">The search query.</param>
        /// <param name="embed">The embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Show> SearchSingleShowAsync(string query, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<Show>("/singlesearch/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Search for a tv show with imdb id.
        /// </summary>
        /// <param name="imdbId">The imdb id of the show.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Show> SearchByIMDbIdAsync(string imdbId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("imdb", imdbId),
            };

            return await GetJsonAsync<Show>("/lookup/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Search for a tv show with tvdb id.
        /// </summary>
        /// <param name="tvdbId">The tvdb id of the show.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Show> SearchByTVDbIdAsync(string tvdbId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("thetvdb", tvdbId),
            };

            return await GetJsonAsync<Show>("/lookup/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Search for a tv show with tvrage id.
        /// </summary>
        /// <param name="tvRageId">The tvrage id of the show.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Show> SearchByTVRageIdAsync(string tvRageId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("tvrage", tvRageId),
            };

            return await GetJsonAsync<Show>("/lookup/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Search for people.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<SearchPerson>> SearchPeopleAsync(string query)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            return await GetJsonAsync<IList<SearchPerson>>("/search/people", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// The schedule is a complete list of episodes that air in a given country on a given date.
        /// Episodes are returned in the order in which they are aired, and full information about the episode and the corresponding show is included.
        /// </summary>
        /// <remarks>
        /// Note that contrary to what you might expect, the ISO country code for the United Kingdom is not UK, but GB.
        /// </remarks>
        /// <param name="dateTime">The schedule date (defaults to the present day).</param>
        /// <param name="country">The schedule country (defaults to "US").</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Schedule>> GetScheduleAsync(DateTime? dateTime = null, string country = null)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (dateTime.HasValue)
            {
                // Date is an ISO 8601 formatted date; defaults to the current day.
                parameters.Add(new KeyValuePair<string, string>("date", dateTime.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(country))
            {
                // CountryCode is an ISO 3166-1 code of the country; defaults to US.
                parameters.Add(new KeyValuePair<string, string>("country", country));
            }

            return await GetJsonAsync<IList<Schedule>>("/schedule", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// The web schedule is a complete list of episodes that air on web/streaming channels on a given date.
        /// TVmaze distinguishes between local and global Web Channels: local Web Channels are only available in one
        /// specific country, while global Web Channels are available in multiple countries.
        /// </summary>
        /// <remarks>
        /// To query both local and global Web Channels, leave out the country parameter.
        /// To query only local Web Channels, set country to an ISO country code.
        /// And to query only global Web Channels, set country to an empty string.
        /// </remarks>
        /// <param name="dateTime">The schedule date (defaults to the present day).</param>
        /// <param name="country">The schedule country (defaults to "US").</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Schedule>> GetStreamingScheduleAsync(DateTime? dateTime = null, string country = null)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (dateTime.HasValue)
            {
                // Date is an ISO 8601 formatted date; defaults to the current day.
                parameters.Add(new KeyValuePair<string, string>("date", dateTime.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(country))
            {
                // CountryCode is an ISO 3166-1 code of the country; defaults to US.
                parameters.Add(new KeyValuePair<string, string>("country", country));
            }

            return await GetJsonAsync<IList<Schedule>>("/schedule/web", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// The full schedule is a list of all future episodes known to TVmaze, regardless of their country.
        /// Be advised that this endpoint's response is at least several MB large.
        /// As opposed to the other endpoints, results are cached for 24 hours.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Schedule>> GetFullScheduleAsync()
        {
            return await GetJsonAsync<IList<Schedule>>("/schedule/full")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all primary information for a given show.
        /// </summary>
        /// <remarks>
        /// This endpoint allows embedding of additional information.
        /// </remarks>
        /// <param name="showId">The show id.</param>
        /// <param name="embed">The embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Show> GetShowInfoAsync(int showId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<Show>($"/shows/{showId}", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// A complete list of episodes for the given show.
        /// Episodes are returned in their airing order, and include full episode information.
        /// By default, specials are not included in the list.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <param name="specials">Should specials be included.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Episode>> GetShowEpisodesListAsync(int showId, bool specials = false)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (specials)
            {
                parameters.Add(new KeyValuePair<string, string>("specials", "1"));
            }

            return await GetJsonAsync<IList<Episode>>($"/shows/{showId}/episodes", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve one specific episode from this show given its season number and episode number.
        /// This either returns the full information for one episode, or a HTTP 404.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <param name="season">The season number.</param>
        /// <param name="episode">The episode number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Episode> GetShowEpisodeAsync(int showId, int season, int episode)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("season", season.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("number", episode.ToString(CultureInfo.InvariantCulture)),
            };

            return await GetJsonAsync<Episode>($"/shows/{showId}/episodebynumber", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all episodes from this show that have aired on a specific date.
        /// This either returns an array of full episode info, or a HTTP 404.
        /// Useful for daily (talk) shows that don't adhere to a common season numbering.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <param name="dateTime">The episode date.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Episode>> GetShowEpisodesByDateAsync(int showId, DateTime dateTime)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("date", dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            };

            return await GetJsonAsync<IList<Episode>>($"/shows/{showId}/episodesbydate", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// A complete list of seasons for the given show.
        /// Seasons are returned in ascending order and contain the full information that's known about them.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Season>> GetShowSeasonsAsync(int showId)
        {
            return await GetJsonAsync<IList<Season>>($"/shows/{showId}/seasons")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// A list of episodes in this season. Specials are always included in this list;
        /// they can be recognized by a NULL value for number.
        /// </summary>
        /// <param name="seasonId">The season id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Episode>> GetSeasonEpisodesAsync(int seasonId)
        {
            return await GetJsonAsync<IList<Episode>>($"/seasons/{seasonId}/episodes")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// A list of main cast for a show.
        /// Each cast item is a combination of a person and a character.
        /// Items are ordered by importance, which is determined by the total number of appearances of the given character in this show.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Cast>> GetShowCastAsync(int showId)
        {
            return await GetJsonAsync<IList<Cast>>($"/shows/{showId}/cast")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// A list of main crew for a show.
        /// Each crew item is a combination of a person and their crew type.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Crew>> GetShowCrewAsync(int showId)
        {
            return await GetJsonAsync<IList<Crew>>($"/shows/{showId}/crew")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// A list of AKA's (aliases) for a show. An AKA with its country set to null indicates an AKA in the show's original country.
        /// Otherwise, it's the AKA for that show in the given foreign country.
        /// </summary>
        /// <param name="showId">The show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<ShowAlias>> GetShowAliasesAsync(int showId)
        {
            return await GetJsonAsync<IList<ShowAlias>>($"/shows/{showId}/akas")
                .ConfigureAwait(false);
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
        /// <param name="page">The page number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<Show>> GetShowIndexAsync(int page = 1)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("page", page.ToString(CultureInfo.InvariantCulture)),
            };

            return await GetJsonAsync<IList<Show>>("/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all primary information for a given episode.
        /// This endpoint allows embedding of additional information.
        /// </summary>
        /// <param name="episodeId">The episode id.</param>
        /// <param name="embed">The embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Episode> GetEpisodeByIdAsync(int episodeId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<Episode>($"/episodes/{episodeId}", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all primary information for a given person.
        /// This endpoint allows embedding of additional information.
        /// </summary>
        /// <param name="personId">The person id.</param>
        /// <param name="embed">The embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Person> GetPersonInfoAsync(int personId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<Person>($"/people/{personId}", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all (show-level) cast credits for a person.
        /// A cast credit is a combination of both a show and a character.
        /// By default, only a reference to each show and character will be returned.
        /// However, this endpoint supports embedding, which means full information for the shows and characters can be included.
        /// </summary>
        /// <param name="personId">The person id.</param>
        /// <param name="embed">The embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<CastCredits>> GetCastCreditsAsync(int personId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<IList<CastCredits>>($"/people/{personId}/castcredits", parameters)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all (show-level) crew credits for a person.
        /// A crew credit is combination of both a show and a crew type.
        /// By default, only a reference to each show will be returned.
        /// However, this endpoint supports embedding, which means full information for the shows can be included.
        /// </summary>
        /// <param name="personId">The person id.</param>
        /// <param name="embed">The embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IList<CrewCredits>> GetCrewCreditsAsync(int personId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<IList<CrewCredits>>($"/people/{personId}/crewcredits", parameters)
                .ConfigureAwait(false);
        }

        private static string GetParametersString(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in parameters)
            {
                builder.Append(builder.Length == 0 ? "?" : "&");
                builder.Append($"{item.Key}={item.Value}");
            }

            return builder.ToString();
        }

        private static void AddEmbeddedPrametersToList(string[] embed, List<KeyValuePair<string, string>> parameters)
        {
            if (embed is null)
            {
                throw new ArgumentNullException(nameof(embed));
            }

            if (embed.Length == 1)
            {
                parameters.Add(new KeyValuePair<string, string>("embed", embed[0]));
                return;
            }

            foreach (var item in embed)
            {
                parameters.Add(new KeyValuePair<string, string>("embed[]", item));
                return;
            }
        }

        private async Task<T> GetJsonAsync<T>(string requestUrl, List<KeyValuePair<string, string>> parameters = null)
        {
            string url = _options.ApiAddress + requestUrl;

            if (parameters is null)
            {
                parameters = new List<KeyValuePair<string, string>>();
            }

            // Add api key if defined to list
            if (!string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                parameters.Add(new KeyValuePair<string, string>("apikey", _options.ApiKey));
            }

            // Concat parameters to URL
            url += GetParametersString(parameters);

            using var response = await _httpClient.GetAsync(new Uri(url))
                .ConfigureAwait(false);

            // TODO: Maybe handle API Rate limit (429)?
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
