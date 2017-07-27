using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SsoPortal
{
    public static class ClaimsExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal principal, PortalClaimTypes type)
        {
            return principal.GetClaimValue<string>(type);
        }

        public static T GetClaimValue<T>(this ClaimsPrincipal principal, PortalClaimTypes type)
        {
            var claim = principal.FindFirst(type.ToString());

            if (claim == null) return default(T);

            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }

        public static void UpdateClaims(this ClaimsIdentity identity, List<Claim> claims)
        {
            claims.ForEach(x => identity.UpdateClaim(x.Type, x.Value));
        }

        public static void UpdateClaim(this ClaimsIdentity identity, string type, string value)
        {
            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(type);

            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(type, value));
        }
    }

    public enum PortalClaimTypes
    {
        UserId,
        DefaultPatId,
        AvailablePatIds,
        FirstName,
        LastName,
        MiddleName,
        Role
    }
}
