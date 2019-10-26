namespace ExpensesCounter.Common.Models.Auth
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; }

        public AccessTokenResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}