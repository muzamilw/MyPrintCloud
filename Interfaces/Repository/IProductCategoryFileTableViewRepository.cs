using System;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IProductCategoryFileTableViewRepository : IBaseRepository<CategoryFileTableView, Guid>
    {
        /// <summary>
        /// Get File by Stream Id
        /// </summary>
        CategoryFileTableView GetByStreamId(Guid streamId);

        /// <summary>
        /// Returns New Path for directory/file to be stored 
        /// </summary>
        string GetNewPathLocator(string path, string fileTableName);
    }
}
