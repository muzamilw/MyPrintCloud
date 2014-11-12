
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Job Type Service Interface
    /// </summary>
    public interface IJobTypeService
    {

        /// <summary>
        /// Search Job Type
        /// </summary>
        JobTypeSearchRequestResponse SearchJobType(JobTypeSearchRequest request);

        /// <summary>
        /// Delete Job Type by id
        /// </summary>
        void DeleteJobType(long jobTypeId);

        /// <summary>
        /// Add /Update JobType
        /// </summary>
        JobType SaveJobType(JobType jobType);

    }
}
