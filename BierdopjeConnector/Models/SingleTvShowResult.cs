namespace SemanticArchitecture.Subtitle.Models
{
    using System;
    using System.Xml.Serialization;

    using Chalk.SubtitlesManagement.Models;

    [XmlRoot(ElementName = "bierdopje", Namespace = "")]
    [Serializable]
    public class SingleTvShowResult : ISingleTvShowResult
    {
        [XmlElement(ElementName = "response")]
        public TvShow tvShow;

        public TvShowBase TvShow
        {
            get { return tvShow; }
        }
    }
}