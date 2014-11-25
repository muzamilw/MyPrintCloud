namespace MPC.Models.Common
{
    /// <summary>
    /// Access Right Claim Value
    /// </summary>
    public class AccessRightClaimValue
    {
        /// <summary>
        /// Right Id
        /// </summary>
        public long RightId { get; set; }
        
        /// <summary>
        /// Right Name
        /// </summary>
        public string Right { get; set; }

        /// <summary>
        /// Section Id
        /// </summary>
        public long SectionId { get; set; }

        /// <summary>
        /// Section Name
        /// </summary>
        public string Section { get; set; }
    }
}
