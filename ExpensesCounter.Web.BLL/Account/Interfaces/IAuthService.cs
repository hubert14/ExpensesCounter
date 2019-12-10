using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Auth;

namespace ExpensesCounter.Web.BLL.Account.Interfaces
{
    public interface IAuthService
    {
        Task<TokensResponse> LoginAsync(LoginModel loginModel);
        Task<AccessTokenResponse> LoginAsync(string refreshToken);

        Task<TokensResponse> RegisterAsync(RegisterModel registerModel);
    }
}