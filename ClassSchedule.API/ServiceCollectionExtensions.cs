using ClassSchedule.Application.Implementations;
using ClassSchedule.Application.Interfaces;
using ClassSchedule.Application.Mappings;
using ClassSchedule.Application.Services;
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
            services.AddAutoMapper(typeof(MappingSchedule));
            //services.AddSingleton<ILogger, LoggerService>();
            //services.AddSingleton(typeof(ILogger<>), typeof(LoggerService<>));
            services.AddSingleton<LoggerService>();

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
