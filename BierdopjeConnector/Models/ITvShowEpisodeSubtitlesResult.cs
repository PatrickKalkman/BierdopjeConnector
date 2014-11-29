namespace SemanticArchitecture.Subtitle.Models
{
    using System.Collections.Generic;

    using Chalk.SubtitlesManagement.Models;

    public interface ITvShowEpisodeSubtitlesResult
   {
      List<TvShowEpisodeSubtitle> TvShowEpisodeSubtitles { get; }
   }
}