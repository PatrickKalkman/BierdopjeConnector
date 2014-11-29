using System;
using System.Xml.Serialization;

namespace Chalk.SubtitlesManagement.Models
{
    using SemanticArchitecture.Subtitle.Models;

    [XmlRoot("bierdopje")]
    [Serializable]
    public class SingleTvShowEpisodeResult : ITvShowEpisode
    {
        [XmlElement("response")]
        public TvShowEpisode tvShowEpisode;

        [XmlIgnore]
        public TvShowEpisode Episode
        {
            get { return tvShowEpisode; }
        }
    }
}