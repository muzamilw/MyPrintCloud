namespace MPC.Models.Common
{
    /// <summary>
    /// User Identity Model
    /// </summary>
    public class UserIdentityModel
    {
        /// <summary>
        /// User Info
        /// </summary>
        public MisUser User { get; set; }

        /// <summary>
        /// Is User Authenticated
        /// </summary>
        public bool IsAuthenticated { get; set; }
    }
}
