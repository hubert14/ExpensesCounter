namespace ExpensesCounter.Common.Models.Auth
{
    public class TokensResponse
    {
        public TokensResponse(string access, string refresh)
        {
            AccessToken = access;
            RefreshToken = refresh;
        }

        public string AccessToken { get; }

        public string RefreshToken { get; }
    }
}