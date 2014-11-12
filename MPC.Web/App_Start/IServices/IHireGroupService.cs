using System;
using Cares.Models.DomainModels;
using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Hire Group Interface
    /// </summary>
    public interface IHireGroupService
    {
        /// <summary>
        /// Get Hire Groups By Vehicle Make, Category, Model, Year and Hire Group Code
        /// For AutoComplete
        /// </summary>
        IEnumerable<HireGroupDetail> GetByCodeAndVehicleInfo(string searchText, long operationWorkPlaceId, DateTime startDtTime,
            DateTime endDtTime);

        /// <summary>
        /// Load roups, based on search filters
        /// </summary>
        HireGroupSearchResponse LoadHireGroups(HireGroupSearchRequest tariffTypeRequest);
        
        /// <summary>
        /// Load Hire Group Base data
        /// </summary>
        /// <returns></returns>
        HireGroupBaseResponse LoadBaseData();
        
        /// <summary>
        /// Find Hire Group By ID
        /// </summary>
        /// <param name="hireGroupId"></param>
        /// <returns></returns>
        HireGroup FindById(long hireGroupId);
        
        /// <summary>
        /// Delete Hire Group
        /// </summary>
        /// <param name="instance"></param>
        void DeleteHireGroup(HireGroup instance);
        
        /// <summary>
        /// Add Hire Group
        /// </summary>
        /// <param name="hireGroup"></param>
        HireGroup AddHireGroup(HireGroup hireGroup);
       
        /// <summary>
        /// Update Hire Group
        /// </summary>
        /// <param name="hireGroup"></param>
        HireGroup UpdateHireGroup(HireGroup hireGroup);
        
        /// <summary>
        /// Get Hire Group Deatil Data By id
        /// </summary>
        /// <param name="hireGroupId"></param>
        /// <returns></returns>
        HireGroupDataDetailResponse FindHireGroupId(long hireGroupId);
    }
}
