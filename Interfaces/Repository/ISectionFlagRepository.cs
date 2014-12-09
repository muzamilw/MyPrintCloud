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
        IEnumerable<SectionFlag> GetSectionFlagBySectionId(long SectionId);
    }
}
