using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetClaimValue(this ClaimsPrincipal principal, string claimType) =>
            principal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value ?? string.Empty;
    }
}
