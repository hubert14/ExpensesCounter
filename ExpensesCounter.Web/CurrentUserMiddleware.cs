using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpensesCounter.Web.BLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExpensesCounter.Web
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, CurrentUserInfo currentUserInfo)
        {
            var idClaim =
                httpContext.User.Claims.FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.NameIdentifier))?.Value;

            if (!string.IsNullOrWhiteSpace(idClaim) && int.TryParse(idClaim, out var userId))
                currentUserInfo.Id = userId;

            return _next(httpContext);
        }
    }

    public static class CurrentUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseCurrentUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CurrentUserMiddleware>();
        }
    }
}