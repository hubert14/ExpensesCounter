using ExpensesCounter.Web.BLL.Account.Interfaces;
using ExpensesCounter.Web.BLL.Account.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesCounter.Web.BLL.DI
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}