using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace CompanyEmployees.Extensions
{
    /*An extension method is inherently a static method.
     * What makes it
different from other static methods is that it accepts this as the first
parameter, and this represents the data type of the object which will be
using that extension method.
     */
    //An extension method must be defined inside a static class.
    //Once we define an extension method, it can be chained multiple times on the same type of object.

    public static class ServiceExtensions
    {
        //CORS (Cross-Origin Resource Sharing) is a mechanism to give or restrict access rights to applications from different domains.
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });
        public static void ConfigureLoggerService(this IServiceCollection services) =>
 services.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
 services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>
services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
IConfiguration configuration) =>
services.AddDbContext<RepositoryContext>(opts =>
opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}
