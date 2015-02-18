namespace MPC.Common
{
    /// <summary>
    /// MPC Claim Types
    /// </summary>
    public class MpcClaimTypes
    {
        /// <summary>
        /// Company 
        /// </summary>
        public const string Organisation = "http://schemas.2xb.com/2012/07/identity/claims/organisation";

        /// <summary>
        /// Access right
        /// </summary>
        public const string AccessRight = "http://schemas.2xb.com/2012/07/identity/claims/accessRight";
        
        /// <summary>
        /// MIS Roles
        /// </summary>
        public const string MisRole = "http://schemas.2xb.com/2012/07/identity/claims/misRole";

        /// <summary>
        /// User Name
        /// </summary>
        public const string MisUser = "http://schemas.2xb.com/2012/07/identity/claims/misUser";

        /// <summary>
        /// System User Id
        /// </summary>
        public const string SystemUser = "http://schemas.2xb.com/2012/07/identity/claims/systemUser";

        /// <summary>
        /// Is User under Trial Period
        /// </summary>
        public const string IsTrial = "http://schemas.2xb.com/2012/07/identity/claims/isTrial";

        /// <summary>
        /// User Trial count
        /// </summary>
        public const string TrialCount = "http://schemas.2xb.com/2012/07/identity/claims/trialCount";


    }
}
