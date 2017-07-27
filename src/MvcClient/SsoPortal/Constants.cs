

namespace SsoPortal
{
    public static class Constants
    {
        public static readonly string ClientId = "mvc";
        public static readonly string ClientSecret = "secret";
        public static readonly string Authority = "http://vsmskTwebprtl3:81/SsoPortal";
        public static readonly string ReturnUrl = "http://vsmskTwebprtl3:81/MvcClient/signin-oidc";
        public static readonly string PostLogoutRedirectUri = "http://vsmskTwebprtl3:81/MvcClient/signout-callback-oidc";

        public static readonly string[] ClaimsToExclude = { "aud", "iss", "nbf", "exp", "nonce", "iat", "at_hash" };

        // Minimum 900 to match server side token expiration
        public static readonly int CookieAuthExpirationSeconds = 900;
    }
}