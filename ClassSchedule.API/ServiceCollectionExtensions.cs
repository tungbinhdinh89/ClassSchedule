using ClassSchedule.Application.Implementations;
using ClassSchedule.Application.Interfaces;
using ClassSchedule.Application.Mappings;
using ClassSchedule.Application.Services;
using ClassSchedule.Application.Services.ClassSchedule.Application.Services;
using ClassSchedule.Core.DB;
using Microsoft.EntityFrameworkCore;
namespace ClassSchedule.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddTransient<TransientService>();
            services.AddSingleton<SingletonService>();
            services.AddAutoMapper(typeof(MappingSchedule));

            return services;
        }

        public static IServiceCollection AddApplicationDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");
            return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        }
    }
}
