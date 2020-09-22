﻿using System;
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
        private readonly TVMazeConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        public TVMazeService(HttpClient httpClient)
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = new TVMazeConfiguration();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration">An instance of <see cref="TVMazeConfiguration"/>.</param>
        public TVMazeService(HttpClient httpClient, TVMazeConfiguration configuration)
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <inheritdoc/>
        public async Task<IList<Search>> SearchShowsAsync(string query)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            string json = await GetJsonAsync("/search/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Search>>(json);
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

            string json = await GetJsonAsync("/singlesearch/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        /// <inheritdoc/>
        public async Task<Show> SearchByIMDbIdAsync(string imdbId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("imdb", imdbId),
            };

            string json = await GetJsonAsync("/lookup/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        /// <inheritdoc/>
        public async Task<Show> SearchByTVDbIdAsync(string tvdbId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("thetvdb", tvdbId),
            };

            string json = await GetJsonAsync("/lookup/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        /// <inheritdoc/>
        public async Task<Show> SearchByTVRageIdAsync(string tvRageId)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("tvrage", tvRageId),
            };

            string json = await GetJsonAsync("/lookup/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<SearchPerson>> SearchPeopleAsync(string query)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("q", System.Web.HttpUtility.UrlEncode(query)),
            };

            string json = await GetJsonAsync("/search/people", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<SearchPerson>>(json);
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

            string json = await GetJsonAsync("/schedule", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Schedule>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Schedule>> GetFullScheduleAsync()
        {
            string json = await GetJsonAsync("/schedule/full")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Schedule>>(json);
        }

        /// <inheritdoc/>
        public async Task<Show> GetShowInfoAsync(int id, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            string json = await GetJsonAsync($"/shows/{id}", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Show>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetShowEpisodesListAsync(int showId, bool specials = false)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (specials == true)
            {
                parameters.Add(new KeyValuePair<string, string>("specials", "1"));
            }

            string json = await GetJsonAsync($"/shows/{showId}/episodes", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Episode>>(json);
        }

        /// <inheritdoc/>
        public async Task<Episode> GetShowEpisodeAsync(int showId, int season, int episode)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("season", season.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("number", episode.ToString(CultureInfo.InvariantCulture)),
            };

            string json = await GetJsonAsync($"/shows/{showId}/episodebynumber", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Episode>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetShowEpisodesByDateAsync(int showId, DateTime dateTime)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("date", dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            };

            string json = await GetJsonAsync($"/shows/{showId}/episodesbydate", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Episode>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Season>> GetShowSeasonsAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/seasons")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Season>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetSeasonEpisodesAsync(int seasonId)
        {
            string json = await GetJsonAsync($"/seasons/{seasonId}/episodes")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Episode>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Cast>> GetShowCastAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/cast")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Cast>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Crew>> GetShowCrewAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/crew")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Crew>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<ShowAlias>> GetShowAliasesAsync(int showId)
        {
            string json = await GetJsonAsync($"/shows/{showId}/akas")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<ShowAlias>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<Show>> GetShowIndexAsync(int page = 1)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("page", page.ToString(CultureInfo.InvariantCulture)),
            };

            string json = await GetJsonAsync("/shows", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<Show>>(json);
        }

        /// <inheritdoc/>
        public async Task<Episode> GetEpisodeByIdAsync(int episodeId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            string json = await GetJsonAsync($"/episodes/{episodeId}", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Episode>(json);
        }

        /// <inheritdoc/>
        public async Task<Person> GetPersonInfoAsync(int personId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            string json = await GetJsonAsync($"/people/{personId}", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Person>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<CastCredits>> GetCastCreditsAsync(int personId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            string json = await GetJsonAsync($"/people/{personId}/castcredits", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<CastCredits>>(json);
        }

        /// <inheritdoc/>
        public async Task<IList<CrewCredits>> GetCrewCreditsAsync(int personId, params string[] embed)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (embed != null && embed.Length != 0)
            {
                AddEmbeddedPrametersToList(embed, parameters);
            }

            string json = await GetJsonAsync($"/people/{personId}/crewcredits", parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IList<CrewCredits>>(json);
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

        private async Task<string> GetJsonAsync(string requestUrl, List<KeyValuePair<string, string>> parameters = null)
        {
            string url = _configuration.BaseAddress + requestUrl;

            if (parameters is null)
            {
                parameters = new List<KeyValuePair<string, string>>();
            }

            // Add api key if defined to list
            if (!string.IsNullOrWhiteSpace(_configuration.APIKey))
            {
                parameters.Add(new KeyValuePair<string, string>("apikey", _configuration.APIKey));
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
    }
}
