using System;
using System.Security.Claims;

namespace Services.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserID(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(id))
                return null;

            return Int32.Parse(id);
        }

        public static Guid? GetUserGuidID(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var guidID = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(guidID))
                return null;

            return Guid.Parse(guidID);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static int? GetCompanyID(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var companyID = principal.FindFirst(ClaimTypes.GroupSid)?.Value;

            if (string.IsNullOrEmpty(companyID))
                return null;

            return int.Parse(companyID);
        }
    }
}
