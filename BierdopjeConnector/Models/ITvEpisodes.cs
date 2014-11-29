namespace SemanticArchitecture.Subtitle.Models
{
    using System.Collections.Generic;

    using Chalk.SubtitlesManagement.Models;

    public interface ITvEpisodes
   {
      List<TvShowEpisode> TvEpisodes { get; }
   }
}