using Microsoft.Extensions.DependencyInjection;

namespace HRApp.Common
{
    public static class Extensions
    {
        public static IServiceCollection AddSystemClock(this IServiceCollection services)
        {
            services.AddSingleton<ISystemClock, DefaultSystemClock>();
            return services;
        }
    }
}
