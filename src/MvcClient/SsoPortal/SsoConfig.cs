using Microsoft.AspNetCore.Builder;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace SsoPortal
{
    public static class SsoConfig
    {
        public static void UseSsoPortal(this IApplicationBuilder app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                CookieName = Constants.ClientId,
                ExpireTimeSpan = TimeSpan.FromSeconds(Constants.CookieAuthExpirationSeconds),
                SlidingExpiration = true,
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = Constants.Authority,
                RequireHttpsMetadata = false,

                ClientId = Constants.ClientId,
                ClientSecret = Constants.ClientSecret,

                ResponseType = "code id_token",
                Scope = { "api_portalsvc", "offline_access" },

                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true,
                //Events = new OpenIdConnectEvents
                //{
                //    OnTicketReceived = context =>
                //    {
                //        context.Principal.Identities.First().UpdateClaims(
                //            claimService.CustomClaims(
                //                context.Principal.GetClaimValue(PortalClaimTypes.UserId),
                //                context.Principal.GetClaimValue(PortalClaimTypes.DefaultPatId)));

                //        return Task.CompletedTask;
                //    }
                //}
            });
        }
    }
}