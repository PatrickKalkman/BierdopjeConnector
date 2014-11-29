// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Semantic Architecture http://www.semanticarchitecture.net">
//      Semantic Architecture
// </copyright>
// <summary>
//   Defines a specific logger that uses log4net to log messages from the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SemantichArchitecture.Logger
{
    using SemanticArchitecture.Logging;

    using log4net;

    public class Logger : ILogger
    {
        private const string LoggerName = "DefaultLogger";

        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Log(string message)
        {
            LogManager.GetLogger(LoggerName).Info(message);
        }

        public void WriteLine(string format, object args0)
        {
            LogManager.GetLogger(LoggerName).Info(string.Format(format, args0));
        }

        public void WriteLine(string format, object args0, object args1)
        {
            LogManager.GetLogger(LoggerName).Info(string.Format(format, args0, args1));
        }
    }
}