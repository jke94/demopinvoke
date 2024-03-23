namespace pinvoke.consoleapp
{
    #region using

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using pinvoke.consoleapp.Native;
    using pinvoke.consoleapp.Services;

    #endregion

    public class Program
    {
        #region Methods

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<INativeWrapper, NativeWrapper>();
                services.AddTransient<IMainService, MainService>();
            });

        #endregion

        #region Main method

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var main_service = host.Services.GetRequiredService<IMainService>();

            main_service.Run();

            host.Dispose();
        }

        #endregion
    }
}