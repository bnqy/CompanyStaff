using Contracts;
using LoggerService;

namespace CompanyStaff.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigCORS(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicyAnyOrigin", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

        public static void ConfigIISIntegration(this IServiceCollection services) => 
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigLoggerService(this IServiceCollection services) => 
            services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
