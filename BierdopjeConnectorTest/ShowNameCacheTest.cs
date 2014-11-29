namespace SemanticArchitecture.Subtitles
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Rhino.Mocks;

    using SemanticArchitecture.Subtitle;

    [TestFixture]
    public class ShowNameCacheTest
    {
        private const string DummyFileName = "DummyFileName";
        private const string DummyContents = "DummyContents";
        private readonly Dictionary<string, int> dummyCache = new Dictionary<string, int>();

        private MockRepository mockRepository;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository();
        }

        [Test]
        public void ShouldInitializeCacheByReadingFile()
        {
            var fileRepository = mockRepository.StrictMock<IFileRepository>();
            var cacheSerializer = mockRepository.Stub<ICacheSerializer>();
            using (mockRepository.Record())
            {
                fileRepository.Expect(r => r.ReadFile(DummyFileName)).IgnoreArguments().Return(DummyContents);
            }

            using (mockRepository.Playback())
            {
                var showNameCache = new ShowNameCache(fileRepository, cacheSerializer);
                showNameCache.Initialize();
            }
        }

        [Test]
        public void ShouldDeserializeCacheOnInitialize()
        {
            var fileRepository = mockRepository.Stub<IFileRepository>();
            var cacheSerialization = mockRepository.StrictMock<ICacheSerializer>();
            using (mockRepository.Record())
            {
                cacheSerialization.Expect(s => s.Deserialize(DummyContents)).IgnoreArguments().Return(dummyCache);
            }

            using (mockRepository.Playback())
            {
                var showNameCache = new ShowNameCache(fileRepository, cacheSerialization);
                showNameCache.Initialize();
            }
        }

        [Test]
        public void ShouldSerializeCacheOnSave()
        {
            var fileRepository = mockRepository.Stub<IFileRepository>();
            var cacheSerialization = mockRepository.StrictMock<ICacheSerializer>();
            using (mockRepository.Record())
            {
                cacheSerialization.Expect(c => c.Serialize(dummyCache)).Return(string.Empty).IgnoreArguments();
            }

            using (mockRepository.Playback())
            {
                var showNameCache = new ShowNameCache(fileRepository, cacheSerialization);
                showNameCache.Save();
            }
        }

        [Test]
        public void ShouldSaveCacheContentsToFileSystemOnSave()
        {
            var fileRepository = mockRepository.StrictMock<IFileRepository>();
            var cacheSerialization = mockRepository.Stub<ICacheSerializer>();
            using (mockRepository.Record())
            {
                cacheSerialization.Stub(c => c.Serialize(dummyCache)).IgnoreArguments().Return(DummyContents);
                fileRepository.Expect(f => f.SaveFile(DummyContents, DummyFileName)).IgnoreArguments();
            }

            using (mockRepository.Playback())
            {
                var showNameCache = new ShowNameCache(fileRepository, cacheSerialization);
                showNameCache.Save();
            }
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void AddShouldAddTheItemToCache(int itemsToAdd)
        {
            var showIdCache = CreateShowNameCache();
            for (int addCounter = 0; addCounter < itemsToAdd; addCounter++)
            {
                showIdCache.Add(string.Format("Lie To Me {0}", addCounter), addCounter);
            }

            Assert.That(showIdCache.NumberOfCachedShows, Is.EqualTo(itemsToAdd));
        }

        [Test]
        public void TryGetShouldReturnItemAndTrueWhenItemIsFound()
        {
            var showIdCache = CreateShowNameCache();
            const string ShowName = "Lie To Me";
            const int LieToMeShowId = 1;
            showIdCache.Add(ShowName, LieToMeShowId);
            int showId;
            bool result = showIdCache.TryGetShowId(ShowName, out showId);
            Assert.That(result, Is.True);
            Assert.That(showId, Is.EqualTo(LieToMeShowId));
        }

        private ShowNameCache CreateShowNameCache()
        {
            var fileRepository = mockRepository.Stub<IFileRepository>();
            var cacheSerialization = mockRepository.Stub<ICacheSerializer>();
            var showIdCache = new ShowNameCache(fileRepository, cacheSerialization);
            return showIdCache;
        }
    }
}
