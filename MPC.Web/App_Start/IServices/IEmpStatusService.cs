
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Employee Status Service Interface
    /// </summary>
    public interface IEmpStatusService
    {
    
        /// <summary>
        /// Search Employee Status
        /// </summary>
        EmpSearchRequestResponse SearchEmployeeStatus(EmpSearchRequest request);

        /// <summary>
        /// Delete Employee Status by id
        /// </summary>
        void DeleteEmployeeStatus(long empStatusId);

        /// <summary>
        /// Add /Update Employee Status
        /// </summary>
        EmpStatus SaveEmpStatus(EmpStatus empStatus);

    }
}
