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

        public static IServiceCollection AddTimer(this IServiceCollection services)
        {
            services.AddTransient<ITimer, ThreadingTimer>();
            return services;
        }

        public static IServiceCollection AddTaskManager(this IServiceCollection services)
        {
            services.AddSingleton<ITaskManager, TaskManager>();
            return services;
        }
    }
}
