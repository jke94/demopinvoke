namespace pinvoke.wpfuiapp
{
    #region using

    using pinvoke.nativewrapperlibrary;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Windows;
    using pinvoke.wpfuiapp.ViewModel;
    using pinvoke.wpfuiapp.Services;
    using System;
    using Microsoft.Extensions.Hosting;
    using pinvoke.wpfuiapp.Logger;

    #endregion

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private Fields

        private readonly IHost _host;

        #endregion

        #region Constructor

        public App()
        {
            _host = CreateWpfHostBuilder().Build();
        }

        #endregion

        #region Public Methods

        private static IHostBuilder CreateWpfHostBuilder() =>
            Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                SetCustomServices(services);
            })
            .ConfigureAppConfiguration((ctx, cfg) =>
            {
                cfg.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",
                optional: false,
                reloadOnChange: true);
            })
            .ConfigureLogging((hostBuilder, configureLogging) =>
            {
                configureLogging.AddCustomLogger(hostBuilder.Configuration);
            });

        #endregion

        #region Private Methods

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainViewModel = _host.Services.GetRequiredService<IMainViewModel>();

            Resources.Add("MyMainViewModel", mainViewModel);

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(10));
            }
        }

        private static void SetCustomServices(IServiceCollection services)
        {
            services.AddNativeWrapperServices();
            services.AddLogging();
            services.AddScoped<IMainViewModel, MainViewModel>();
            services.AddScoped<IMainService, MainService>();
        }
        #endregion
    }
}
