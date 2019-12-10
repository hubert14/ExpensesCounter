using System;
using System.Collections.Generic;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<ExpensesListUser> ExpensesLists { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public DateTimeOffset RegisterDateUtc { get; set; }
        public DateTimeOffset LastLoginUtc { get; set; }

        public bool IsEnabled { get; set; }


        public static void GenerateExceptionIfNull(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User must have a value");
        }
    }
}