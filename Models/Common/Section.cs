using System.Collections.Generic;

namespace MPC.Models.Common
{
    /// <summary>
    /// Section
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Section Id
        /// </summary>
        public long SectionId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Access Rights - access rights User Has
        /// </summary>
        public ICollection<AccessRight> AccessRights { get; set; } 
    }
}
