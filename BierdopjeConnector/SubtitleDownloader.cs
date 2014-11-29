// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleDownloader.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   This class is responsible for downloading subtitles of a Show.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    using Chalk.SubtitlesManagement;
    using Chalk.SubtitlesManagement.Models;

    using SemanticArchitecture.Logging;
    using SemanticArchitecture.Subtitle.Models;

    /// <summary>
    /// This class is responsible for downloading subtitles of a Show.
    /// </summary>
    public class SubtitleDownloader
    {
        private readonly ShowService showService;
        private readonly SubtitleDownloadConfiguration configuration;
        private readonly ILogger logger;

        public SubtitleDownloader(ShowService showService, ILogger logger, SubtitleDownloadConfiguration configuration)
        {
            this.showService = showService;
            this.logger = logger;
            this.configuration = configuration;
        }

        public void DownloadSubtitle(string name, int episode, int season)
        {
            TvShowBase show = this.showService.FindShowByName(name);

            if (show != null)
            {
                List<TvShowEpisode> episodesForShow = this.showService.GetEpisodesForSeason(show.id, season);
                foreach (TvShowEpisode showEpisode in episodesForShow)
                {
                    if (showEpisode.episode == episode.ToString())
                    {
                        string language = string.Empty;
                        if (showEpisode.subsen)
                        {
                            language = "en";
                        } 
                        
                        if (showEpisode.subsnl)
                        {
                            language = "nl";
                        }
                            
                        logger.WriteLine("Downloading {0} subtitles for {1}.", language, showEpisode.title);
                        DownloadSubtitleForEpisode(showEpisode, language);

                        if (string.IsNullOrEmpty(language))
                        {
                            logger.WriteLine("No subtitles found for {0} from {1}", show.showName, showEpisode.title);
                        }
                    }
                }
            }
            else
            {
                logger.WriteLine("No shows found with name {0}", name);
            }
        }

        public void Initialize()
        {
            showService.Initialize();
        }

        private void DownloadSubtitleForEpisode(TvShowEpisode showEpisode, string language)
        {
            List<TvShowEpisodeSubtitle> episodeSubtitles = this.showService.GetAllSubsForEpisode(showEpisode.episodeId, language);
            using (var webClient = new WebClient())
            {
                foreach (TvShowEpisodeSubtitle episodeSubtitle in episodeSubtitles)
                {
                    if (!Directory.Exists(configuration.DownloadFolder))
                    {
                        Directory.CreateDirectory(configuration.DownloadFolder);
                    }

                    string subTitlePath = Path.Combine(configuration.DownloadFolder, episodeSubtitle.fileName) + ".srt";
                    if (!File.Exists(subTitlePath))
                    {
                        webClient.DownloadFile(episodeSubtitle.downloadLink, subTitlePath);
                    }
                    else
                    {
                        logger.WriteLine("Skipping file {0}, the file was already downloaded.", episodeSubtitle.fileName);
                    }
                }
            }
        }
    }
}