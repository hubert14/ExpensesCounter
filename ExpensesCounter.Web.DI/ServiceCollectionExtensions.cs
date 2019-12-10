using ExpensesCounter.Web.BLL;
using ExpensesCounter.Web.DAL;
using ExpensesCounter.Web.Utils.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesCounter.Web.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCurrentUserInfo(this IServiceCollection services)
        {
            services.AddScoped<CurrentUserInfo>();
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            BLL.DI.ServicesCollectionExtensions.AddServices(services);
            
            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));

            using (var ctx = new ApplicationContext(connectionString)) ctx.Database.Migrate();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
           
            services.AddSingleton(MapperHelper.ConfiguredMapper);

            return services;
        }
    }
}