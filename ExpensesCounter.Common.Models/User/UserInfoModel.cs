using System;

namespace ExpensesCounter.Common.Models.User
{
    public class UserInfoModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string FirstName { get; set; }
        public string LastName  { get; set; }

        public DateTimeOffset RegisterDateUtc { get; set; }
        public DateTimeOffset LastLoginUtc    { get; set; }
    }
}