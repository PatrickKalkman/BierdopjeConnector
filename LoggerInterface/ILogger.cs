// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="http://www.semanticarchitecture.net semantic architecture">
//  Semantic Architecture
// </copyright>
// <summary>
//   Defines a generic logging interface to shield the application from directly using a logging framework such as 
//   Log4Net
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemanticArchitecture.Logging
{
    public interface ILogger
    {
        void WriteLine(string format, object args0);

        void WriteLine(string format, object args0, object args1);
    }
}
