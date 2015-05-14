using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Section Flag Repository Inteface
    /// </summary>
    public interface ISectionFlagRepository : IBaseRepository<SectionFlag, long>
    {

        /// <summary>
        /// Get Section Flag For Inventory
        /// </summary>
        IEnumerable<SectionFlag> GetSectionFlagForInventory();

        /// <summary>
        /// Get Section Flag By Section Id
        /// </summary>
        IEnumerable<SectionFlag> GetSectionFlagBySectionId(long sectionId);

         /// <summary>
        /// Get Defualt Section Flag for Price Matrix in webstore
        /// </summary>
        int GetDefaultSectionFlagId();

        /// <summary>
        /// Get Section Flag For Customer Price Index Section
        /// </summary>
        IEnumerable<SectionFlag> GetAllForCustomerPriceIndex();
        IEnumerable<SectionFlag> GetDefaultSectionFlags();
        /// <summary>
        /// Get Section Flags for Campaign
        /// </summary>
        IEnumerable<SectionFlag> GetAllForCampaign();
        List<SectionFlag> GetSectionFlagsByOrganisationID(long OID);

        SectionFlag GetSectionFlag(long id);

         /// <summary>
        /// Get Defualt Section Flag for Price Matrix in webstore by organisation Id
        /// </summary>
        int GetDefaultSectionFlagId(long OrganisationId);
    }
}
