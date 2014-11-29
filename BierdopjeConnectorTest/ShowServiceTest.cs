namespace SemanticArchitecture.Subtitles
{
    using System.Collections.Generic;

    using Chalk.SubtitlesManagement.Models;

    using NUnit.Framework;

    using Rhino.Mocks;

    using SemanticArchitecture.Logging;
    using SemanticArchitecture.Subtitle;
    using SemanticArchitecture.Subtitle.Models;

    [TestFixture]
    public class ShowServiceTest
    {
        private readonly MockRepository repository = new MockRepository();

        [Test]
        public void ShouldSendResponseStringToFindShowByNameInResponseParser()
        {
            const string ResponseString = "Dummy Response String";

            var responseParser = repository.DynamicMock<SubtitleServiceResponseDeserializer>();
            Expect.Call(responseParser.GetTvShows(ResponseString)).Return(new List<TvShow>());
            var logger = repository.Stub<ILogger>();

            var tvShows = repository.DynamicMock<IBierdopje>();
            Expect.Call(tvShows.FindShowByName(string.Empty)).Return(ResponseString).IgnoreArguments();

            var showNameCache = repository.Stub<ShowNameCache>(new object[] { null, null });

            var showService = new ShowService(responseParser, tvShows, showNameCache, logger);
            repository.ReplayAll();
            showService.FindShowsByName("Flash");
            repository.VerifyAll();
        }

        [Test]
        public void ShouldCacheShowNameAfterFind()
        {
            var deserializer = repository.Stub<SubtitleServiceResponseDeserializer>();
            var bierdopjeService = repository.Stub<IBierdopje>();
            var logger = repository.Stub<ILogger>();

            const string ShowName = "Lie To Me";
            const int ShowId = 50;

            var showNameCache = repository.StrictMock<ShowNameCache>(new object[] { null, null });
            using (repository.Record())
            {
                deserializer.Expect(d => d.GetTvShows(string.Empty)).IgnoreArguments().Return(
                    new List<TvShow>() { new TvShow { id = ShowId, showName = ShowName } });

                int outShowId;
                showNameCache.Expect(s => s.TryGetShowId(ShowName, out outShowId)).Return(false);
                showNameCache.Expect(s => s.Add(ShowName, ShowId));
                showNameCache.Expect(s => s.Save());
            }

            using (repository.Playback())
            {
                var showService = new ShowService(deserializer, bierdopjeService, showNameCache, logger);
                showService.FindShowByName(ShowName);
            }
        }
    }
}