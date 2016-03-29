
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Region Service Interface
    /// </summary>
    public interface IDesignGradeService
    {
       
        /// <summary>
        /// Search Design Grade
        /// </summary>
        DesignGradeSearchRequestResponse SearchDesignGrade(DesignGradeSearchRequest request);

        /// <summary>
        /// Delete Design Grade by id
        /// </summary>
        void DeleteDesignGrade(long designGradeId);

        /// <summary>
        /// Add /Update Design Grade
        /// </summary>
        DesignGrade SaveDesignGrade(DesignGrade designGrade);
    }
}
