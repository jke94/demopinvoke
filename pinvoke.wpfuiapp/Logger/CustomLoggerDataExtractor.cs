﻿namespace pinvoke.wpfuiapp.Logger
{
    #region using

    using Microsoft.Extensions.Logging;
    using System.Globalization;
    using System.IO;

    #endregion

    public class CustomLoggerDataExtractor : ILogger
    {
        #region Private Fields

        protected readonly CustomLoggerProvider _customLoggerProvider;
        private readonly string _fullFileLoggerPath;
        private readonly string _categoryName;

        #endregion

        #region Constructor

        public CustomLoggerDataExtractor(string categoyName, CustomLoggerProvider customLoggerProvider) 
        {
            _categoryName = categoyName;
            _customLoggerProvider = customLoggerProvider;
            _fullFileLoggerPath = _customLoggerProvider.Options.LogFileName.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        #endregion

        #region Public Methods

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => default;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                var messge = $"[{CustomStrDate()}][{logLevel}][{_categoryName}][{formatter(state, exception)}]";

                using var streamWriter = new StreamWriter(_fullFileLoggerPath, true);
                streamWriter.WriteLine(messge);
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
