namespace ExpensesCounter.Common.Models.Auth
{
    public class LoginModel
    {
        public LoginModel(string email, string password, string passwordConfirm)
        {
            Email = email;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }

        public string Email { get; }

        public string Password { get; }
        public string PasswordConfirm { get; }

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(PasswordConfirm) &&
            string.Equals(Password, PasswordConfirm);
    }
}