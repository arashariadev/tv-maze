using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCollection.TVMaze.Models;

namespace MovieCollection.TVMaze
{
    /// <summary>
    /// The <c>ITVMazeService</c> interface.
    /// </summary>
    public interface ITVMazeService
    {
        /// <summary>
        /// Search through all the shows in our database by the show's name.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Search>> SearchShowsAsync(string query);

        /// <summary>
        /// Search for a single tv show.
        /// </summary>
        /// <remarks>
        /// This endpoint allows embedding of additional information.
        /// </remarks>
        /// <param name="query">Search query.</param>
        /// <param name="embed">Embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Show> SearchSingleShowAsync(string query, params string[] embed);

        /// <summary>
        /// Search for a tv show with imdbId.
        /// </summary>
        /// <param name="imdbId">Imdb id of the show.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Show> SearchByIMDbIdAsync(string imdbId);

        /// <summary>
        /// Search for a tv show with tvdbId.
        /// </summary>
        /// <param name="tvdbId">Tvdb id of the show.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Show> SearchByTVDbIdAsync(string tvdbId);

        /// <summary>
        /// Search for a tv show with tvRageId.
        /// </summary>
        /// <param name="tvRageId">tvRage id of the show.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Show> SearchByTVRageIdAsync(string tvRageId);

        /// <summary>
        /// Search for people.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<SearchPerson>> SearchPeopleAsync(string query);

        /// <summary>
        /// The schedule is a complete list of episodes that air in a given country on a given date.
        /// Episodes are returned in the order in which they are aired, and full information about the episode and the corresponding show is included.
        /// Note that contrary to what you might expect, the ISO country code for the United Kingdom is not UK, but GB.
        /// </summary>
        /// <param name="dateTime">Schedule date (Defaults to the current day).</param>
        /// <param name="country">Schedule country (Defaults to US).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Schedule>> GetScheduleAsync(DateTime? dateTime = null, string country = null);

        /// <summary>
        /// The full schedule is a list of all future episodes known to TVmaze, regardless of their country.
        /// Be advised that this endpoint's response is at least several MB large.
        /// As opposed to the other endpoints, results are cached for 24 hours.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Schedule>> GetFullScheduleAsync();

        /// <summary>
        /// Retrieve all primary information for a given show.
        /// </summary>
        /// <remarks>
        /// This endpoint allows embedding of additional information.
        /// </remarks>
        /// <param name="id">Show id.</param>
        /// <param name="embed">Embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Show> GetShowInfoAsync(int id, params string[] embed);

        /// <summary>
        /// A complete list of episodes for the given show.
        /// Episodes are returned in their airing order, and include full episode information.
        /// By default, specials are not included in the list.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <param name="specials">Should specials be included.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Episode>> GetShowEpisodesListAsync(int showId, bool specials = false);

        /// <summary>
        /// Retrieve one specific episode from this show given its season number and episode number.
        /// This either returns the full information for one episode, or a HTTP 404.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <param name="season">Season number.</param>
        /// <param name="episode">Episode number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Episode> GetShowEpisodeAsync(int showId, int season, int episode);

        /// <summary>
        /// Retrieve all episodes from this show that have aired on a specific date.
        /// This either returns an array of full episode info, or a HTTP 404.
        /// Useful for daily (talk) shows that don't adhere to a common season numbering.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <param name="dateTime">Episode date.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Episode>> GetShowEpisodesByDateAsync(int showId, DateTime dateTime);

        /// <summary>
        /// A complete list of seasons for the given show.
        /// Seasons are returned in ascending order and contain the full information that's known about them.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Season>> GetShowSeasonsAsync(int showId);

        /// <summary>
        /// A list of episodes in this season. Specials are always included in this list;
        /// they can be recognized by a NULL value for number.
        /// </summary>
        /// <param name="seasonId">Season id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Episode>> GetSeasonEpisodesAsync(int seasonId);

        /// <summary>
        /// A list of main cast for a show.
        /// Each cast item is a combination of a person and a character.
        /// Items are ordered by importance, which is determined by the total number of appearances of the given character in this show.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Cast>> GetShowCastAsync(int showId);

        /// <summary>
        /// A list of main crew for a show.
        /// Each crew item is a combination of a person and their crew type.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Crew>> GetShowCrewAsync(int showId);

        /// <summary>
        /// A list of AKA's (aliases) for a show. An AKA with its country set to null indicates an AKA in the show's original country.
        /// Otherwise, it's the AKA for that show in the given foreign country.
        /// </summary>
        /// <param name="showId">Show id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<ShowAlias>> GetShowAliasesAsync(int showId);

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
        /// <param name="page">Page number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<Show>> GetShowIndexAsync(int page = 1);

        /// <summary>
        /// Retrieve all primary information for a given episode.
        /// This endpoint allows embedding of additional information.
        /// </summary>
        /// <param name="episodeId">Episode id.</param>
        /// <param name="embed">Embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Episode> GetEpisodeByIdAsync(int episodeId, params string[] embed);

        /// <summary>
        /// Retrieve all primary information for a given person.
        /// This endpoint allows embedding of additional information.
        /// </summary>
        /// <param name="personId">Person id.</param>
        /// <param name="embed">Embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Person> GetPersonInfoAsync(int personId, params string[] embed);

        /// <summary>
        /// Retrieve all (show-level) cast credits for a person.
        /// A cast credit is a combination of both a show and a character.
        /// By default, only a reference to each show and character will be returned.
        /// However, this endpoint supports embedding, which means full information for the shows and characters can be included.
        /// </summary>
        /// <param name="personId">Person id.</param>
        /// <param name="embed">Embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<CastCredits>> GetCastCreditsAsync(int personId, params string[] embed);

        /// <summary>
        /// Retrieve all (show-level) crew credits for a person.
        /// A crew credit is combination of both a show and a crew type.
        /// By default, only a reference to each show will be returned.
        /// However, this endpoint supports embedding, which means full information for the shows can be included.
        /// </summary>
        /// <param name="personId">Person id.</param>
        /// <param name="embed">Embeddings.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IList<CrewCredits>> GetCrewCreditsAsync(int personId, params string[] embed);
    }
}
