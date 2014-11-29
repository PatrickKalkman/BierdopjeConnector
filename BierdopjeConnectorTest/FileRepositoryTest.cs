namespace SemanticArchitecture.Subtitles
{
    using System.IO;

    using NUnit.Framework;

    using SemanticArchitecture.Subtitle;

    [TestFixture]
    public class FileRepositoryTest
    {
        private const string DummyContents = "BlaBlaBla";
        private string tempFileName;

        [SetUp]
        public void SetUp()
        {
            tempFileName = Path.Combine(Path.GetTempPath(), "TvSeriesCache.dat");
        }

        [Test]
        public void ShouldReadFileAndReturnContentsAsString()
        {
            try
            {
                File.WriteAllText(tempFileName, DummyContents);
                var fileRepository = new FileRepository();
                string contents = fileRepository.ReadFile(tempFileName);
                Assert.That(contents, Is.EqualTo(DummyContents));
            }
            finally
            {
                File.Delete(tempFileName);
            }
        }

        [Test]
        public void ShouldWriteStringToFileSystem()
        {
            try
            {
                var fileRepository = new FileRepository();
                fileRepository.SaveFile(tempFileName, DummyContents);
                string readContent = File.ReadAllText(tempFileName);
                Assert.That(readContent, Is.EqualTo(DummyContents));
            }
            finally
            {
                File.Delete(tempFileName);
            }
        }
    }
}
