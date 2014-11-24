namespace MPC.Models.Common
{
    /// <summary>
    /// Role Section
    /// </summary>
    public class RoleSection
    {
        /// <summary>
        /// Section Id
        /// </summary>
        public long SectionId { get; set; }

        /// <summary>
        /// Role Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// Role Section - Sections that user has access rights to
        /// </summary>
        public Section Section { get; set; }
    }
}
