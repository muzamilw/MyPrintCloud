using System.Collections.Generic;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Standard Rate Service Interface
    /// </summary>
    public interface IStandardRateService
    {
        /// <summary>
        /// Find By Hire Group Id and standard Rate Main Id
        /// </summary>
        /// <param name="standardRtMainId"></param>
        /// <param name="hireGroupDetailId"></param>
        /// <returns></returns>
        IEnumerable<StandardRate> FindStandardRate(long standardRtMainId, long hireGroupDetailId);
    }
}
