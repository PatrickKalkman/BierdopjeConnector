// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheSerializer.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   Serializes and deserializes the name + id cache.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    using Chalk.SubtitlesManagement;

    public class CacheSerializer : ICacheSerializer
    {
        private readonly JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();

        public Dictionary<string, int> Deserialize(string stringToDeserialize)
        {
            return scriptSerializer.Deserialize<Dictionary<string, int>>(stringToDeserialize);
        }

        public string Serialize(Dictionary<string, int> dictionary)
        {
            return scriptSerializer.Serialize(dictionary);
        }
    }
}