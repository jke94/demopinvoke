namespace pinvoke.wpfuiapp.Logger
{
    #region using

    using Microsoft.Extensions.Logging;
    using System.Globalization;
    using System.IO;

    #endregion

    public class CustomLoggerDataExtractor : ILogger
    {
        #region Private Fields

        private readonly string _name;

        private readonly string _logFilePath;

        #endregion

        #region Constructor

        public CustomLoggerDataExtractor(string name, string logFilePath) 
        {
            _logFilePath = logFilePath;
            _name = name;
        }

        #endregion

        #region Public Methods

        public IDisposable? BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel) => default;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                var messge = $"[{CustomStrDate()}][{logLevel}][{_name.Split('.')?.Last() ?? _name}][{formatter(state, exception)}]";

                if (File.Exists(_logFilePath))
                {
                    File.AppendAllText(_logFilePath, messge + Environment.NewLine);
                }
            }
            catch (Exception)
            {
                // TODO
            }
        }

        #endregion

        #region Private Methods

        private static string CustomStrDate() => $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}";

        #endregion
    }
}
