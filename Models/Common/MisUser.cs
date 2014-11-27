using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MPC.Models.Common
{
    /// <summary>
    /// Mis User
    /// </summary>
    public class MisUser : IdentityUser
    {
        /// <summary>
        /// Unique key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// Role Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// Organisation Id
        /// </summary>
        public long OrganisationId { get; set; }

        /// <summary>
        /// Role 
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Role Sections - Sections that user has access rights to 
        /// </summary>
        public ICollection<RoleSection> RoleSections { get; set; }

        /// <summary>
        /// Generate User Identity
        /// </summary>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MisUser, string> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        
    }
}
