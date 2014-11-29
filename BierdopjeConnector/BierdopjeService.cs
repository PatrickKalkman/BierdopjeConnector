// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BierdopjeService.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   Defines the BierdopjeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    using System.Globalization;
    using System.Net;

    /// <summary>
    /// This class is responsible for wrapping the functionality of the bierdopje api. Each method
    /// of the bierdopje api can be used and the response is transformed into a strongly typed object.
    /// </summary>
    public class BierdopjeService : IBierdopje
    {
        private readonly SubtitleDownloadConfiguration subtitleDowloaderConfiguration;

        public BierdopjeService(SubtitleDownloadConfiguration subtitleDowloaderConfiguration)
        {
            this.subtitleDowloaderConfiguration = subtitleDowloaderConfiguration;
        }

        public string GetShowById(string showId)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetShowById/{1}", subtitleDowloaderConfiguration.Uri, showId);
                return client.DownloadString(requestUrl);
            }
        }

        public string GetShowByTvDbId(string tvDbShowId)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetShowByTVDBID/{1}", subtitleDowloaderConfiguration.Uri, tvDbShowId);
                return client.DownloadString(requestUrl);
            }
        }

        public string FindShowByName(string name)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/FindShowByName/{1}", subtitleDowloaderConfiguration.Uri, name);
                return client.DownloadString(requestUrl);
            }
        }

        public string GetEpisodesForSeason(string showId, string season)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetEpisodesForSeason/{1}/{2}", subtitleDowloaderConfiguration.Uri, showId, season);
                return client.DownloadString(requestUrl);
            }
        }

        public string GetAllEpisodesForShow(string showId)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetAllEpisodesForShow/{1}", subtitleDowloaderConfiguration.Uri, showId);
                return client.DownloadString(requestUrl);
            }
        }

        public string GetEpisodeById(string episodeId)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetEpisodeById/{1}", subtitleDowloaderConfiguration.Uri, episodeId);
                return client.DownloadString(requestUrl);
            }
        }

        public string GetAllSubsForEpisode(string episodeId, string language)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetAllSubsForEpisode/{1}/{2}", subtitleDowloaderConfiguration.Uri, episodeId, language);
                return client.DownloadString(requestUrl);
            }
        }

        public string GetAllSubsFor(string showId, string season, string episodeId, string language, string isTvDbId)
        {
            using (var client = new WebClient())
            {
                string requestUrl = string.Format(CultureInfo.InvariantCulture, "{0}/GetAllSubsFor/{1}/{2}/{3}/{4}/{5}", subtitleDowloaderConfiguration.Uri, showId, season, episodeId, language, isTvDbId);
                return client.DownloadString(requestUrl);
            }
        }
    }
}
