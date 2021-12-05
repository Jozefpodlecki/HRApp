using HRApp.DAL;
using HRApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRApp.Common
{
    public static class Extensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString, options =>
            {
                options.UseHierarchyId();
            }));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IOutOfOfficeRepository, OutOfOfficeRepository>();
            return services;
        }
    }
}
