// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Semantic Architecture">
//  www.semanticarchitecture.net
// </copyright>
// <summary>
//   THe main entry point of the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle.Console
{
    using System;
    using System.IO;

    using SemanticArchitecture.Logging;
    using SemanticArchitecture.Subtitle.Console.Properties;

    using SemantichArchitecture.Logger;

    using SimpleInjector;

    /// <summary>
    /// The main entry point of the application.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage DownloadSubtitle \"Name of show\" Season Episode");
                Console.WriteLine("\n For example: [DownloadSubtitle \"Lie To Me\" 1 1]\n to download the subtitles of the first episode of the first season");
                return;
            }

            Container container = CreateContainerAndRegisterTypes();
            var subTitleDownloader = container.GetInstance<SubtitleDownloader>();
            subTitleDownloader.Initialize();
            subTitleDownloader.DownloadSubtitle(args[0], int.Parse(args[1]), int.Parse(args[2]));
        }

        private static Container CreateContainerAndRegisterTypes()
        {
            var container = new Container();
            container.RegisterSingle(
                new SubtitleDownloadConfiguration 
                { 
                    DownloadFolder = Path.Combine(Directory.GetCurrentDirectory(), Settings.Default.DownloadFolder),
                    Uri = string.Format("{0}/{1}", Settings.Default.BierdopjeUrl, Settings.Default.ApiKey)
                });
            container.Register<IBierdopje, BierdopjeService>();
            container.Register<IFileRepository, FileRepository>();
            container.Register<ICacheSerializer, CacheSerializer>();
            container.Register<ILogger, Logger>();
            return container;
        }
    }
}