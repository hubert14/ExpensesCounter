namespace ExpensesCounter.Common.Models.Auth
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(PasswordConfirm) &&
            string.Equals(Password, PasswordConfirm);
    }
}