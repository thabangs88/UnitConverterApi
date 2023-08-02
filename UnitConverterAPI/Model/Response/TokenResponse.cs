namespace UnitConverterAPI.Model.Response
{
    public class TokenResponse
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
