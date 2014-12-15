using MPC.Models.DomainModels;

namespace MPC.Models.Common
{
    /// <summary>
    /// Organisation Claim Value
    /// </summary>
    public class OrganisationClaimValue
    {
        /// <summary>
        /// Organisation Id
        /// </summary>
        public long OrganisationId { get; set; }

        public long LoginuserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
            //CompanyContact loginContact { get; set; }
    }
}
