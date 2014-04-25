using System;

namespace ArkyMapService
{
    /// <summary>
    /// Utility class for logging.
    /// </summary>
    internal class Logger
    {
        #region inner class
        /// <summary>
        /// Enumerates the available logging modes.
        /// </summary>
        internal enum LogMode
        {
            /// <summary>
            /// Loging mode to write to console.
            /// </summary>
            Console,


            /// <summary>
            /// Logging mode to write to file.
            /// </summary>
            File
        }
        #endregion


        #region attributes
        private LogMode m_mode;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of a <see cref="Logger"/> class.
        /// </summary>
        /// <param name="mode">Mode of logging method.</param>
        internal Logger(LogMode mode)
        {
            m_mode = mode;
        }
        #endregion


        #region logging
        internal void WriteLog(string formatString, params string[] args)
        {
            string logMessage = string.Format(formatString, args);

            switch (m_mode)
            {
                case LogMode.Console:
                    WriteLogToConsole(logMessage);
                    break;
                case LogMode.File:
                    WriteLogToFile(logMessage);
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region logging modes
        /// <summary>
        /// Writes log message to console.
        /// </summary>
        /// <param name="logMessage">Log message to write.</param>
        private void WriteLogToConsole(string logMessage)
        {
            Console.WriteLine(logMessage);
        }


        /// <summary>
        /// Writes log message into file.
        /// </summary>
        /// <param name="logMessage">Log message to write.</param>
        private void WriteLogToFile(string logMessage)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
