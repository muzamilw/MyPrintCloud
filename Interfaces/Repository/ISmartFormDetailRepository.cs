using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Smart Form Detail Repository Interface
    /// </summary>
    public interface ISmartFormDetailRepository : IBaseRepository<SmartFormDetail,long>
    {
        /// <summary>
        /// Get Smart Form Details By Smart Form Id
        /// </summary>
        IEnumerable<SmartFormDetail> GetSmartFormDetailsBySmartFormId(long smartFormId);
    }
}
