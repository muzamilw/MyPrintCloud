using System;
using System.Collections.Generic;

namespace MPC.Models.Common
{
    /// <summary>
    /// Mis User
    /// </summary>
    public class MisUser
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
        /// The user's email address
        /// </summary>
        public string Email { get; set; }

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
    }
}
