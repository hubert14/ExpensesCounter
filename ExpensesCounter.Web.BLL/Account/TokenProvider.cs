using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Auth;
using ExpensesCounter.Web.DAL;
using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpensesCounter.Web.BLL.Account
{
    internal class TokenProvider
    {
        private readonly ApplicationContext _context;
        private readonly JwtTokenBuilder _tokenBuilder;

        public TokenProvider(ApplicationContext context, JwtTokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<TokensResponse> GenerateTokensAsync(User user)
        {
            User.GenerateExceptionIfNull(user);

            var accessToken = GenerateAccessToken(user);
            var refreshToken = await GenerateNewRefreshTokenAsync(user.Id);
            return new TokensResponse(accessToken, refreshToken);
        }

        public TokensResponse GenerateTokens(User user)
        {
            User.GenerateExceptionIfNull(user);

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateNewRefreshToken(user.Id);
            return new TokensResponse(accessToken, refreshToken);
        }

        public async Task<string> GenerateNewAccessTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException(nameof(refreshToken), "Refresh token must have a value");

            var userToken = await _context.RefreshTokens.Include(entity => entity.User)
                .FirstOrDefaultAsync(entity => entity.Token == refreshToken);
            if (userToken == null) throw new KeyNotFoundException("Refresh token is invalid");

            return GenerateAccessToken(userToken.User);
        }

        public string GenerateNewAccessToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException(nameof(refreshToken), "Refresh token must have a value");

            var userToken = _context.RefreshTokens.Include(entity => entity.User)
                .FirstOrDefault(entity => entity.Token == refreshToken);
            if (userToken == null) throw new KeyNotFoundException("Refresh token is invalid");

            return GenerateAccessToken(userToken.User);
        }

        private string GenerateAccessToken(User user)
        {
            var claims = GenerateClaims(user);

            var newToken = _tokenBuilder.GenerateToken(claims);
            return newToken;
        }

        private async Task<string> GenerateNewRefreshTokenAsync(int userId)
        {
            RemoveExistedRefreshTokens(userId);
            var newToken = new RefreshToken(userId);

            await _context.RefreshTokens.AddAsync(newToken);
            await _context.SaveChangesAsync();

            return newToken.Token;
        }

        private string GenerateNewRefreshToken(int userId)
        {
            RemoveExistedRefreshTokens(userId);
            var newToken = new RefreshToken(userId);

            _context.RefreshTokens.Add(newToken);
            _context.SaveChanges();
            return newToken.Token;
        }

        private void RemoveExistedRefreshTokens(int userId)
        {
            var userTokens = FindUserTokens(userId);
            foreach (var token in userTokens) _context.Entry(token).State = EntityState.Deleted;
        }

        private IEnumerable<RefreshToken> FindUserTokens(int userId)
        {
            return _context.RefreshTokens.Where(token => token.UserId == userId);
        }

        private static IEnumerable<Claim> GenerateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };
        }
    }
}