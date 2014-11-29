namespace SemanticArchitecture.Subtitle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable()]
    [XmlRoot("bierdopje")]
    public class TvShowEpisodeSubtitleResult : ITvShowEpisodeSubtitlesResult
    {
        [XmlElement("response")]
        public TvShowEpisodeSubtitleResponse response;

        [XmlIgnore]
        public List<TvShowEpisodeSubtitle> TvShowEpisodeSubtitles
        {
            get { return response.tvShowEpisodeSubtitles; }
        }
    }
}