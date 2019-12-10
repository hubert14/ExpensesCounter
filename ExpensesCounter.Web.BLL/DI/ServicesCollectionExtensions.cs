using ExpensesCounter.Web.BLL.Account.Interfaces;
using ExpensesCounter.Web.BLL.Account.Services;
using ExpensesCounter.Web.BLL.Expenses.Interfaces;
using ExpensesCounter.Web.BLL.Expenses.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesCounter.Web.BLL.DI
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IExpensesService, ExpensesService>();

            return services;
        }
    }
}