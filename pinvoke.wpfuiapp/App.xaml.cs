namespace pinvoke.wpfuiapp
{
    #region using

    using pinvoke.nativewrapperlibrary;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Windows;
    
    #endregion

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private Fields

        public IServiceProvider? ServiceProvider { get; private set; }

        public IConfiguration? Configuration { get; private set; }

        #endregion

        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", 
                optional: false, 
                reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        #endregion

        #region Private Methods

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddNativeWrapperServices();
        }

        #endregion
    }

}
