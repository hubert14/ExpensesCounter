using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Auth;

namespace ExpensesCounter.Web.BLL.Account.Interfaces
{
    public interface IAuthService
    {
        TokensResponse       Login(LoginModel loginModel);
        Task<TokensResponse> LoginAsync(LoginModel loginModel);

        AccessTokenResponse       Login(string refreshToken);
        Task<AccessTokenResponse> LoginAsync(string refreshToken);

        TokensResponse       Register(RegisterModel registerModel);
        Task<TokensResponse> RegisterAsync(RegisterModel registerModel);
    }
}