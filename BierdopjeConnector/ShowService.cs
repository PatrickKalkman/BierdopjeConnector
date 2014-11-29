// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowService.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   This class provides services for downloading subtitles.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Chalk.SubtitlesManagement;
    using Chalk.SubtitlesManagement.Models;

    using SemanticArchitecture.Logging;
    using SemanticArchitecture.Subtitle.Models;

    /// <summary> 
    /// This class provides services for downloading subtitles.
    /// </summary>
    public class ShowService
    {
        private readonly SubtitleServiceResponseDeserializer responseParser;
        private readonly IBierdopje bierdopjeService;
        private readonly ShowNameCache showNameCache;
        private readonly ILogger logger;

        public ShowService(SubtitleServiceResponseDeserializer responseParser, IBierdopje bierdopjeService, ShowNameCache showNameCache, ILogger logger)
        {
            this.responseParser = responseParser;
            this.bierdopjeService = bierdopjeService;
            this.showNameCache = showNameCache;
            this.logger = logger;
        }

        public virtual void Initialize()
        {
            showNameCache.Initialize();
        }

        public virtual List<TvShow> FindShowsByName(string name)
        {
            try
            {
                string responseString = bierdopjeService.FindShowByName(name);
                return responseParser.GetTvShows(responseString);
            }
            catch (Exception error)
            {
                logger.WriteLine("An error occurred while calling FindShowsByName for {0} Error: {1}", name, error.ToString());
                return new List<TvShow>();
            }
        }

        public virtual TvShowBase FindShowByName(string name)
        {
            try
            {
                int showId;
                TvShowBase tvShow;
                if (!showNameCache.TryGetShowId(name, out showId))
                {
                    string responseString = bierdopjeService.FindShowByName(name);
                    List<TvShow> showsFound = responseParser.GetTvShows(responseString);
                    tvShow = showsFound.FirstOrDefault(t => string.Compare(name, t.showName, true, CultureInfo.InvariantCulture) == 0);
                    if (tvShow != null)
                    {
                        showNameCache.Add(name, tvShow.id);
                        showNameCache.Save();
                    }
                }
                else
                {
                    string responseString = bierdopjeService.GetShowById(showId.ToString(CultureInfo.InvariantCulture));
                    tvShow = responseParser.GetTvShow(responseString);
                }

                return tvShow;
            }
            catch (Exception error)
            {
                logger.WriteLine("An error occurred while calling FindShowByName for {0} Error: {1}", name, error.ToString());
                return null;
            }
        }

        public virtual bool TryGetShowById(int id, out TvShowBase show)
        {
            string responseString = bierdopjeService.GetShowById(id.ToString(CultureInfo.InvariantCulture));
            show = responseParser.GetTvShow(responseString);
            return show.id != 0;
        }

        public virtual bool TryGetShowByTvDbId(int tvDbId, out TvShowBase show)
        {
            string responseString = bierdopjeService.GetShowByTvDbId(tvDbId.ToString(CultureInfo.InvariantCulture));
            show = responseParser.GetTvShow(responseString);
            return show.id != 0;
        }

        public virtual List<TvShowEpisode> GetEpisodesForSeason(int showId, int season)
        {
            string responseString = bierdopjeService.GetEpisodesForSeason(showId.ToString(CultureInfo.InvariantCulture), season.ToString(CultureInfo.InvariantCulture));
            return responseParser.GetTvShowEpisodes(responseString);
        }

        public virtual List<TvShowEpisode> GetAllEpisodesForShow(int showId)
        {
            string responseString = bierdopjeService.GetAllEpisodesForShow(showId.ToString(CultureInfo.InvariantCulture));
            return responseParser.GetTvShowEpisodes(responseString);
        }

        public TvShowEpisode GetEpisodeById(int episodeId)
        {
            string responseString = bierdopjeService.GetEpisodeById(episodeId.ToString(CultureInfo.InvariantCulture));
            return responseParser.GetTvShowEpisode(responseString);
        }

        public List<TvShowEpisodeSubtitle> GetAllSubsForEpisode(int episodeId, string language)
        {
            string responseString = bierdopjeService.GetAllSubsForEpisode(episodeId.ToString(), language);
            return responseParser.GetTvShowEpisodeSubtitles(responseString);
        }

        public List<TvShowEpisodeSubtitle> GetAllSubsFor(int showId, int season, int episodeId, string language, bool isTvBdId)
        {
            string responseString = bierdopjeService.GetAllSubsFor(showId.ToString(CultureInfo.InvariantCulture), season.ToString(CultureInfo.InvariantCulture), episodeId.ToString(CultureInfo.InvariantCulture), language, isTvBdId.ToString(CultureInfo.InvariantCulture));
            return responseParser.GetTvShowEpisodeSubtitles(responseString);
        }
    }
}