// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileRepository.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   An interface to not directly use the file system from the application but to abstract it using this repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SemanticArchitecture.Subtitle
{
    public interface IFileRepository
    {
        string ReadFile(string fileName);

        void SaveFile(string fileName, string contents);
    }
}