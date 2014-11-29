// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileRepository.cs" company="SemanticArchitecture http://www.semanticarchitecture.net">
//   SemanticArchitecture
// </copyright>
// <summary>
//   The file repository acts as a repository to the file system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Subtitle
{
    using System.IO;

    using Chalk.SubtitlesManagement;

    public class FileRepository : IFileRepository
    {
        public string ReadFile(string fileName)
        {
            return File.Exists(fileName) ? File.ReadAllText(fileName) : string.Empty;
        }

        public void SaveFile(string fileName, string contents)
        {
            File.WriteAllText(fileName, contents);
        }
    }
}