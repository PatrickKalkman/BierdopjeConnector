// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleDownloaderConfiguration.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   Defines the configuration of the application such as the folder to store the downloaded subtitles and the
//   url to the bierdopje web site.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    public class SubtitleDownloadConfiguration
    {
        public string DownloadFolder
        {
            get; set;
        }

        public string Uri
        {
            get; set; 
        }
    }
}