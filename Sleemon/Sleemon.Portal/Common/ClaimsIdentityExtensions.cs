using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Sleemon.Portal.Common
{
    public static class ClaimsIdentityExtensions
    {
        public const string AvatarClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/avatar";

        public static ClaimsIdentity AsClaimsIdentity(this IIdentity identity)
        {
            return identity as ClaimsIdentity;
        }

        public static string GetUserUniqueId(this ClaimsIdentity identity)
        {
            var claim =
                identity.Claims.SingleOrDefault(
                    item => item.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase));

            return claim == null ? string.Empty : claim.Value;
        }

        public static string GetAvatar(this ClaimsIdentity identity)
        {
            var claim = identity.Claims.SingleOrDefault(item => item.Type.Equals(AvatarClaim, StringComparison.OrdinalIgnoreCase));
            return claim == null ? null : claim.Value;
        }
    }
}
