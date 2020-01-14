using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCollection.TVMaze.Models;

namespace MovieCollection.TVMaze
{
    public interface ITVMazeService
    {
        Task<IList<Search>> SearchShowsAsync(string query);
        Task<Show> SearchSingleShowAsync(string query, params string[] embed);
        Task<Show> SearchByIMDbIdAsync(string imdbId);
        Task<Show> SearchByTVDbIdAsync(string tvdbId);
        Task<Show> SearchByTVRageIdAsync(string tvRageId);
        Task<IList<SearchPerson>> SearchPeopleAsync(string query);
        Task<IList<Schedule>> GetScheduleAsync(DateTime? dateTime = null, string country = null);
        Task<IList<Schedule>> GetFullScheduleAsync();
        Task<Show> GetShowInfoAsync(int id, params string[] embed);
        Task<IList<Episode>> GetShowEpisodesListAsync(int showId, bool specials = false);
        Task<Episode> GetShowEpisodeAsync(int showId, int season, int episode);
        Task<IList<Episode>> GetShowEpisodesByDateAsync(int showId, DateTime dateTime);
        Task<IList<Season>> GetShowSeasonsAsync(int showId);
        Task<IList<Episode>> GetSeasonEpisodesAsync(int seasonId);
        Task<IList<Cast>> GetShowCastAsync(int showId);
        Task<IList<Crew>> GetShowCrewAsync(int showId);
        Task<IList<Alias>> GetShowAliasesAsync(int showId);
        Task<IList<Show>> GetShowIndexAsync(int page = 1);
        Task<Episode> GetEpisodeByIdAsync(int episodeId, params string[] embed);
        Task<Person> GetPersonInfoAsync(int personId, params string[] embed);
        Task<IList<CastCredits>> GetCastCreditsAsync(int personId, params string[] embed);
        Task<IList<CrewCredits>> GetCrewCreditsAsync(int personId, params string[] embed);
    }
}
