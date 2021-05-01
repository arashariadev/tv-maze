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
    public class TVMazeService : ITVMazeService
    {
        private readonly HttpClient _httpClient;
        private readonly TVMazeOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        public TVMazeService(HttpClient httpClient)
            : base()
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
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc/>
        public async Task<IList<Search>> SearchShowsAsync(string query)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            return await GetJsonAsync<IList<Search>>("/search/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<Show> SearchByIMDbIdAsync(string imdbId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("imdb", imdbId),
            };

            return await GetJsonAsync<Show>("/lookup/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Show> SearchByTVDbIdAsync(string tvdbId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("thetvdb", tvdbId),
            };

            return await GetJsonAsync<Show>("/lookup/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Show> SearchByTVRageIdAsync(string tvRageId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("tvrage", tvRageId),
            };

            return await GetJsonAsync<Show>("/lookup/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<SearchPerson>> SearchPeopleAsync(string query)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            return await GetJsonAsync<IList<SearchPerson>>("/search/people", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<IList<Schedule>> GetFullScheduleAsync()
        {
            return await GetJsonAsync<IList<Schedule>>("/schedule/full")
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Show> GetShowInfoAsync(int id, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            return await GetJsonAsync<Show>($"/shows/{id}", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetShowEpisodesListAsync(int showId, bool specials = false)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (specials == true)
            {
                parameters.Add(new KeyValuePair<string, string>("specials", "1"));
            }

            return await GetJsonAsync<IList<Episode>>($"/shows/{showId}/episodes", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetShowEpisodesByDateAsync(int showId, DateTime dateTime)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("date", dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            };

            return await GetJsonAsync<IList<Episode>>($"/shows/{showId}/episodesbydate", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<Season>> GetShowSeasonsAsync(int showId)
        {
            return await GetJsonAsync<IList<Season>>($"/shows/{showId}/seasons")
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetSeasonEpisodesAsync(int seasonId)
        {
            return await GetJsonAsync<IList<Episode>>($"/seasons/{seasonId}/episodes")
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<Cast>> GetShowCastAsync(int showId)
        {
            return await GetJsonAsync<IList<Cast>>($"/shows/{showId}/cast")
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<Crew>> GetShowCrewAsync(int showId)
        {
            return await GetJsonAsync<IList<Crew>>($"/shows/{showId}/crew")
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<ShowAlias>> GetShowAliasesAsync(int showId)
        {
            return await GetJsonAsync<IList<ShowAlias>>($"/shows/{showId}/akas")
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<Show>> GetShowIndexAsync(int page = 1)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("page", page.ToString(CultureInfo.InvariantCulture)),
            };

            return await GetJsonAsync<IList<Show>>("/shows", parameters)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
