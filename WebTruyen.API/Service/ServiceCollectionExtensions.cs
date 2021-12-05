using Microsoft.Extensions.DependencyInjection;

namespace WebTruyen.API.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImageService(this IServiceCollection services)
        {
            return services.AddTransient<IStorageService, FileService>();
        }
    }
}
