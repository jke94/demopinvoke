namespace pinvoke.wpfuiapp.Logger
{
    #region using

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    #endregion

    public static class CustomLoggerProviderExtensions
    {
        public static ILoggingBuilder AddCustomLogger(
            this ILoggingBuilder loggingBuilder,
            Action<LoggerFileOptions> configure)
        {
            loggingBuilder.Services.AddSingleton<ILoggerProvider, CustomLoggerProvider>();
            loggingBuilder.Services.Configure(configure);

            return loggingBuilder;
        }
    }
}
