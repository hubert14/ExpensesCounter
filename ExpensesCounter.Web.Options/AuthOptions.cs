namespace ExpensesCounter.Web.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public double LifetimeInMinutes { get; set; }
    }
}
