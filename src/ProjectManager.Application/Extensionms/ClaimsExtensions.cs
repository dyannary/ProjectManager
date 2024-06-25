using System.Security.Claims;
using System.Security.Principal;
using System;

namespace ProjectManager.Application.Extensionms
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this IPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(claim.Value);
        }
    }
}
