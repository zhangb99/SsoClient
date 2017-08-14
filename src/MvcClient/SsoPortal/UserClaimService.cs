using System.Collections.Generic;
using System.Security.Claims;

namespace SsoPortal
{
    public interface IUserClaimService
    {
        IEnumerable<Claim> GetAppUserClaims(string userId, string patId);
    }

    public class UserClaimService : IUserClaimService
    {
        public IEnumerable<Claim> GetAppUserClaims(string userId, string patId)
        {
            // get from app specific user/role/claim.  This does NOT work for CheckProxyStatus.
            return new List<Claim>
            {
                new Claim(PortalClaimTypes.SelectedPatId.ToString(), "36119464"),
                new Claim("PortalAccess", "appt,msg,med,acct")
            };
        }
    }
}
