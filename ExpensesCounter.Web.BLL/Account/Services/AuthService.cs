using System;
using System.Linq;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Auth;
using ExpensesCounter.Web.BLL.Account.Interfaces;
using ExpensesCounter.Web.DAL;
using ExpensesCounter.Web.DAL.Entities;
using ExpensesCounter.Web.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExpensesCounter.Web.BLL.Account.Services
{
    internal class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly TokenProvider      _tokenProvider;

        public AuthService(ApplicationContext context, IOptions<AuthOptions> authOptions)
        {
            _context = context;

            var tokenBuilder = new JwtTokenBuilder(authOptions.Value.SecurityKey)
            {
                Audience = authOptions.Value.Audience,
                Issuer   = authOptions.Value.Issuer,
                LifeTime = TimeSpan.FromMinutes(authOptions.Value.LifetimeInMinutes)
            };

            _tokenProvider = new TokenProvider(context, tokenBuilder);
        }

        public TokensResponse Login(LoginModel loginModel)
        {
            if (!loginModel.IsValid) throw new ArgumentException("Login model is invalid");

            var existedUser = _context.Users.FirstOrDefault(user =>
                                                                user.Email.ToUpper() ==
                                                                loginModel.Email.Trim().ToUpper());

            if (existedUser == null || !PasswordHasher.VerifyPassword(loginModel.Password, existedUser.PasswordHash))
                throw new ArgumentException("Passwords don't match");

            return _tokenProvider.GenerateTokens(existedUser);
        }

        public async Task<TokensResponse> LoginAsync(LoginModel loginModel)
        {
            if (!loginModel.IsValid) throw new ArgumentException("Login model is invalid");

            var existedUser = await _context.Users.FirstOrDefaultAsync(user =>
                                                                           user.Email.ToUpper() ==
                                                                           loginModel.Email.Trim().ToUpper());

            if (existedUser == null || !PasswordHasher.VerifyPassword(loginModel.Password, existedUser.PasswordHash))
                throw new ArgumentException("Passwords don't match");

            return await _tokenProvider.GenerateTokensAsync(existedUser);
        }

        public AccessTokenResponse Login(string refreshToken)
        {
            return _tokenProvider.GenerateNewAccessToken(refreshToken);
        }

        public Task<AccessTokenResponse> LoginAsync(string refreshToken)
        {
            return _tokenProvider.GenerateNewAccessTokenAsync(refreshToken);
        }


        public TokensResponse Register(RegisterModel registerModel)
        {
            var newUser = MapToUser(registerModel);

            _context.Users.Add(newUser);
            return _tokenProvider.GenerateTokens(newUser);
        }

        public async Task<TokensResponse> RegisterAsync(RegisterModel registerModel)
        {
            var newUser = MapToUser(registerModel);

            await _context.Users.AddAsync(newUser);
            return await _tokenProvider.GenerateTokensAsync(newUser);
        }

        private static User MapToUser(RegisterModel model)
        {
            var hash = PasswordHasher.CreateHash(model.Password);

            return new User
            {
                Email           = model.Email,
                PasswordHash    = hash,
                FirstName       = model.FirstName,
                LastName        = model.LastName,
                PhoneNumber     = model.PhoneNumber,
                Birthday        = model.Birthday,
                RegisterDateUtc = DateTimeOffset.UtcNow,
                LastLoginUtc    = DateTimeOffset.UtcNow,
                CreatedDateUtc  = DateTimeOffset.UtcNow
            };
        }
    }
}