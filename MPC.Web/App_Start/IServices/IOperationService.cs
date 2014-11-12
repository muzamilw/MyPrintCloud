using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Operation Service Interface
    /// </summary>
    public interface IOperationService
    {
        /// <summary>
        /// Get All Opertaions
        /// </summary>
        IEnumerable<Operation> LoadAll();

        /// <summary>
        /// Load Operation BaseData
        /// </summary>
        OperationBaseDataResponse LoadOperationBaseData();

        /// <summary>
        /// Search Operation
        /// </summary>
        OperationSearchResponse SearchOperation(OperationSearchRequest request);

        /// <summary>
        /// Delete Operation
        /// </summary>
        void DeleteOperation(long operationoId);

        /// <summary>
        /// Save Operation
        /// </summary>
        Operation SaveOperation(Operation operation);


    }
}
