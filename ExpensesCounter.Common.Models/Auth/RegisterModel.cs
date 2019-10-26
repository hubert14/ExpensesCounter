using System;

namespace ExpensesCounter.Common.Models.Auth
{
    public class RegisterModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}