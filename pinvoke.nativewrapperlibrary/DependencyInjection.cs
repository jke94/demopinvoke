namespace pinvoke.nativewrapperlibrary
{
    #region using

    using Microsoft.Extensions.DependencyInjection;
    using pinvoke.nativewrapperlibrary.Native;

    #endregion

    public static class DependencyInjection
    {
        public static IServiceCollection AddNativeWrapperServices(this IServiceCollection services)
        {
            services.AddSingleton<INativeWrapper, NativeWrapper>();
            return services;
        }
    }
}