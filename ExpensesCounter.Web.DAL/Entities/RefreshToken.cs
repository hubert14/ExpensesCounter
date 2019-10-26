using System;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class RefreshToken
    {
        public RefreshToken()
        {
        }

        public RefreshToken(int userId, string token)
        {
            UserId = userId;
            Token = token;

            CreatedDateUtc = DateTimeOffset.UtcNow;
        }

        public RefreshToken(int userId)
        {
            UserId = userId;
            Token = Guid.NewGuid().ToString();

            CreatedDateUtc = DateTimeOffset.UtcNow;
        }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Token { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }
    }
}