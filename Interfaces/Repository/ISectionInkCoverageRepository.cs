using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Section Ink Covergae Repository 
    /// </summary>
    public interface ISectionInkCoverageRepository : IBaseRepository<SectionInkCoverage, int>
    {
        List<SectionInkCoverage> GetInkCoveragesBySectionId(long SectionId);
    }
}
