namespace SemanticArchitecture.Subtitles
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using SemanticArchitecture.Subtitle;

    [TestFixture]
    public class CacheSerializerTest
    {
        private const string SerieName = "Lie To Me";
        private const int SerieId = 50;
        
        [Test]
        public void ShouldSerializeCacheToJson()
        {
            var testDictionary = new Dictionary<string, int>();
            testDictionary[SerieName] = SerieId;
            var cacheSerializer = new CacheSerializer();
            string serializedContents = cacheSerializer.Serialize(testDictionary);
            Assert.That(serializedContents, Is.EqualTo(CreateJsonString()));
        }

        [Test]
        public void ShouldDeserializeJsonToCache()
        {
            string json = CreateJsonString();
            var cacheSerializer = new CacheSerializer();
            Dictionary<string, int> result = cacheSerializer.Deserialize(json);
            int id = result[SerieName];
            Assert.That(id, Is.EqualTo(SerieId));
        }

        private static string CreateJsonString()
        {
            return string.Format("{{\"{0}\":{1}}}", SerieName, SerieId);
        }
    }
}