namespace SwiftMart.API.Helpers
{
    public class JwtSettings
    {
        /// <summary>
        /// Secret key used to sign JWT tokens (keep it secret and secure)
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token expiration time in minutes
        /// </summary>
        public int ExpirationMinutes { get; set; }

        /// <summary>
        /// Issuer of the token
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audience for the token
        /// </summary>
        public string Audience { get; set; }
    }
}
