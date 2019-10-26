namespace ExpensesCounter.Common.Models.Auth
{
    public class TokensResponse : AccessTokenResponse
    {
        public TokensResponse(string access, string refresh) : base(access)
        {
            RefreshToken = refresh;
        }
        
        public TokensResponse(AccessTokenResponse accessTokenResponse, string refresh) : base(accessTokenResponse.AccessToken)
        {
            RefreshToken = refresh;
        }

        public string RefreshToken { get; }
    }
}