using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Web;
using MPC.Models.Common;

namespace MPC.Interfaces.MISServices
{

    /// <summary>
    /// Service that adds security claims to the 
    /// </summary>
    public interface IClaimsSecurityService
    {
        /// <summary>
        /// Handles the <see cref="WSFederationAuthenticationModule.SecurityTokenValidated"/> event
        /// </summary>
        void SecurityTokenValidated(object sender, SecurityTokenValidatedEventArgs e, string ipAddress);

        /// <summary>
        /// Lookup identity using the claims identity
        /// </summary>
        IClaimsIdentity LookupIdentity(IClaimsIdentity claimsIdentity);

        /// <summary>
        /// Add claims to the identity
        /// </summary>
        void AddClaimsToIdentity(UserIdentityModel identity, IClaimsIdentity claimsIdentity);

        /// <summary>
        /// Lookup name id and provider name
        /// </summary>
        void LookupIdentityClaimValues(IClaimsIdentity claimsIdentity, out string providerName, out string nameIdentifier);
    }
}
