// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICacheSerializer.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   Defines the ICacheSerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SemanticArchitecture.Subtitle
{
    using System.Collections.Generic;

    public interface ICacheSerializer
    {
        Dictionary<string, int> Deserialize(string stringToDeserialize);

        string Serialize(Dictionary<string, int> dictionary);
    }
}