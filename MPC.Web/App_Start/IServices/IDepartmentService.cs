using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Department Service Interface
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// Get All Departments
        /// </summary>
        IEnumerable<Department> LoadAll();

        /// <summary>
        /// Load Department BaseData
        /// </summary>
        DepartmentBaseDataResponse LoadDepartmentBaseData();

        /// <summary>
        /// Search Department
        /// </summary>
        DepartmentSearchRequestResponse SearchDepartment(DepartmentSearchRequest request);

        /// <summary>
        /// Delete Department
        /// </summary>
        void DeleteDepartment(long departmentId);

        /// <summary>
        /// Save or Update Department
        /// </summary>
        Department SaveUpdateDepartment(Department request);
    }
}
