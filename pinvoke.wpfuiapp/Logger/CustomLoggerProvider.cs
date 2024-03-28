namespace pinvoke.wpfuiapp.Logger
{
    #region using
    
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    #endregion

    public class CustomLoggerProvider : ILoggerProvider
    {
        #region Constructor

        public CustomLoggerProvider(IOptions<LoggerFileOptions> options)
        {
            Options = options.Value;
        }

        #endregion

        #region Public properties

        public readonly LoggerFileOptions Options;

        #endregion

        #region Public Methods

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLoggerDataExtractor(this);
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
