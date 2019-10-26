namespace ExpensesCounter.Common.Models.Auth
{
    public class LoginModel
    {
        public LoginModel(string email, string pass, string confirmPass)
        {
            Email = email;
            Password = pass;
            PasswordConfirm = confirmPass;
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