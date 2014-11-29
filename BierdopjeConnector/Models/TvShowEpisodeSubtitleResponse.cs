namespace SemanticArchitecture.Subtitle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable()]
    public class TvShowEpisodeSubtitleResponse
    {
        [XmlArray("results")]
        [XmlArrayItem("result")]
        public List<TvShowEpisodeSubtitle> tvShowEpisodeSubtitles;
    }
}