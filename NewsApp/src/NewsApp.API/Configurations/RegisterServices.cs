using NewsApp.Core.Enums;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Services;
using System.Diagnostics.CodeAnalysis;

namespace NewsApp.API.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class RegisterServices
    {
        public static IServiceCollection ServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            // Register Services Dependency Injection
            services.AddScoped<ICacheService, CacheService>();
            services.AddMemoryCache();

            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<INewsService, NewsService>();

            return services;
        }
    }
}
