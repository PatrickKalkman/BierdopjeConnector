// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowNameCache.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   This class is responsible for caching the names and corresponding id's of the series. Finding a show
//   by its name is expensive in cost of performance. Therefore, once we have the id of a serie we cache it. The
//   cache is stored in the file system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// This class is responsible for caching the names and corresponding id's of the series. Finding a show
    /// by its name is expensive in cost of performance. Therefore, once we have the id of a serie we cache it. The
    /// cache is stored in the file system.
    /// </summary>
    public class ShowNameCache
    {
        private const string ShowNameCacheFileName = "ShowNameCache.dat";

        private readonly IFileRepository fileRepository;
        private readonly ICacheSerializer cacheSerializer;
        private readonly string showNameCacheFullName;

        private Dictionary<string, int> cacheLookup = new Dictionary<string, int>();

        public ShowNameCache(IFileRepository fileRepository, ICacheSerializer cacheSerializer)
        {
            this.fileRepository = fileRepository;
            this.cacheSerializer = cacheSerializer;
            showNameCacheFullName = CreateCacheFullFileName();
        }

        public int NumberOfCachedShows
        {
            get { return cacheLookup.Count; }
        }

        public virtual void Initialize()
        {
            string cacheContents = fileRepository.ReadFile(showNameCacheFullName);
            cacheLookup = this.cacheSerializer.Deserialize(cacheContents) ?? new Dictionary<string, int>();
        }

        public virtual void Save()
        {
            string serializedCache = cacheSerializer.Serialize(cacheLookup);
            fileRepository.SaveFile(showNameCacheFullName, serializedCache);
        }

        public virtual void Add(string showName, int showId)
        {
            cacheLookup[showName] = showId;
        }

        public virtual bool TryGetShowId(string showName, out int showId)
        {
            return cacheLookup.TryGetValue(showName, out showId);
        }

        private static string CreateCacheFullFileName()
        {
            return Path.Combine(Path.GetTempPath(), ShowNameCacheFileName);
        }
    }
}