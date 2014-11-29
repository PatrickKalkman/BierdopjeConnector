namespace SemanticArchitecture.Subtitle.Models
{
    using System.Collections.Generic;

    using Chalk.SubtitlesManagement.Models;

    internal interface ITvShowResult
   {
      List<TvShow> TvShows { get; }
   }
}