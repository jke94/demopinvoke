namespace pinvoke.wpfuiapp.Logger
{
    #region using

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    #endregion

    public static class CustomLoggerProviderExtensions
    {
        public static ILoggingBuilder AddCustomLogger(
            this ILoggingBuilder loggingBuilder, 
            IConfiguration configuration)
        {
            var loggerProvider = new CustomLoggerProvider(configuration);
            loggingBuilder.AddProvider(loggerProvider);

            return loggingBuilder;
        }
    }
}
