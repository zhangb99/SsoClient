using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SsoPortal
{
    public static class SsoConfig
    {
        public static void UseSsoPortal(this IApplicationBuilder app, IUserClaimService userClaimService)
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
                Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        var ci = context.Ticket.Principal.Identity as ClaimsIdentity;

                        ci.AddClaims(userClaimService.CheckProxyStatus(
                            context.Ticket.Principal.GetClaimValue(PortalClaimTypes.UserId),
                            context.Ticket.Principal.GetClaimValue(PortalClaimTypes.DefaultPatId)));

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}