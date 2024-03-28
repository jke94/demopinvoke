namespace pinvoke.wpfuiapp.Logger
{
    #region using
    
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System.Configuration;
    using System.Globalization;
    using System.IO;

    #endregion

    public class CustomLoggerProvider : ILoggerProvider
    {
        #region Private Fields

        private readonly IConfiguration _configuration;
        private readonly string _logPathFile;

        #endregion

        #region Constructor

        public CustomLoggerProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _logPathFile = Path.Combine(
                Environment.CurrentDirectory,
                _configuration.GetSection("LogWpfAppUiFileName").Value ?? "pinvoke.wpfuiapp_default.log");
        }

        #endregion

        #region Public Methods

        public ILogger CreateLogger(string categoryName)
        {
            if (_logPathFile == null)
            {
                throw new ArgumentNullException(nameof(_logPathFile));
            }
            
            if(!File.Exists(_logPathFile))
            {
                File.Create(_logPathFile);
            }

            return new CustomLoggerDataExtractor(categoryName, _logPathFile);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private static string CustomStrDate() => $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture)}";

        #endregion
    }
}
