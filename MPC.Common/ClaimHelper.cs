using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
//using Microsoft.IdentityModel.Claims;

namespace MPC.Common
{
    /// <summary>
    /// Helper for and extension to <see cref="Microsoft.IdentityModel.Claims.Claim"/>
    /// </summary>
    public static class ClaimHelper
    {
        #region Public

        /// <summary>
        /// Serialize the value
        /// </summary>
        public static string Serialize<T>(T value)
            where T : class
        {
            return SerializeHelper.Serialize(value);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        public static T Deserialize<T>(this Claim claim)
            where T : class
        {
            return SerializeHelper.Deserialize<T>(claim.Value);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        public static T Deserialize<T>(string claimValue)
            where T : class
        {
            return SerializeHelper.Deserialize<T>(claimValue);
        }

        /// <summary>
        /// Get claims matching the claim and value type
        /// </summary>
        public static IList<T> GetClaimsByType<T>(string claimType)
            where T : class
        {
            if (string.IsNullOrEmpty(claimType))
            {
                throw new ArgumentException(LanguageResources.InvalidString, "claimType");
            }

            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimHelper_UnexpectedPrincipalType);
            }

            ClaimsIdentity identity = claimsPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimHelper_UnexpectedIdentityType);
            }

            IEnumerable<Claim> claims = identity.Claims.Where(c => c.Type == claimType);
            return claims.Select(claim => claim.Deserialize<T>()).ToList();
        }
        public static bool RemoveClaim(string claimType)
        {
            if (string.IsNullOrEmpty(claimType))
            {
                throw new ArgumentException(LanguageResources.InvalidString, "claimType");
            }

            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimHelper_UnexpectedPrincipalType);
            }

            ClaimsIdentity identity = claimsPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimHelper_UnexpectedIdentityType);
            }

            IEnumerable<Claim> claims = identity.Claims.Where(c => c.Type == claimType);
            foreach (var claim in claims)
            {
                identity.TryRemoveClaim(claim);
            }
            return true;
           
        }
        #endregion
    }
}
