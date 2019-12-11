using System;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly TokenProvider      _tokenProvider;

        // TODO: Add LastLoginDate updating
        public AuthService(ApplicationContext context, IMapper mapper, IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _mapper = mapper;
            
            _tokenProvider = new TokenProvider(context, authOptions.Value);
        }

        public async Task<TokensResponse> LoginAsync(LoginModel loginModel)
        {
            if (!loginModel.IsValid) throw new ArgumentException("Login model is invalid");

            var existedUser =
                await _context.Users.FirstOrDefaultAsync(user => EF.Functions.Like(user.Email.ToUpper(),
                                                                                   loginModel.Email.Trim().ToUpper()));

            if (existedUser == null || !existedUser.IsEnabled ||
                !PasswordHasher.VerifyPassword(loginModel.Password, existedUser.PasswordHash))
                throw new ArgumentException("Passwords don't match");

            return await _tokenProvider.GenerateTokensAsync(existedUser);
        }

        public Task<AccessTokenResponse> LoginAsync(string refreshToken)
        {
            return _tokenProvider.GenerateNewAccessTokenAsync(refreshToken);
        }

        public async Task<TokensResponse> RegisterAsync(RegisterModel registerModel)
        {
            var newUser = _mapper.Map<User>(registerModel);
            
            newUser.PasswordHash = PasswordHasher.CreateHash(registerModel.Password);

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            return await _tokenProvider.GenerateTokensAsync(newUser);
        }
    }
}