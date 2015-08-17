using System;


namespace MPC.Common
{
    public class XeroOAuthTokens
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public bool HasExpired { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
    }
}
