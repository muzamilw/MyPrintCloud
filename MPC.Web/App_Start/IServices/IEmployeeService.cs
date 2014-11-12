using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Employee Service Interface
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Load All Employee based on search criteria
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        EmployeeSearchResponse LoadAll(EmployeeSearchRequest searchRequest);

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="employee"></param>
        void Delete(Employee employee);


        /// <summary>
        /// Add/Edit Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Employee SaveEmployee(Employee employee);

        /// <summary>
        /// Get Base Data
        /// </summary>
        /// <returns></returns>
        EmployeeBaseResponse GetBaseData();

        /// <summary>
        /// Get Employee Detail
        /// </summary>
        /// <returns></returns>
        Employee GetEmployeeDetail(long employeeId);

        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Employee FindById(long employeeId);

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        void DeleteEmployee(Employee employee);
    }
}
