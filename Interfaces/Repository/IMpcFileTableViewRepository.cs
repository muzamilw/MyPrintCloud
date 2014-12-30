using System;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Get Items List View Repository 
    /// </summary>
    public interface IMpcFileTableViewRepository : IBaseRepository<MpcFileTableView, Guid>
    {
        /// <summary>
        /// Get File by Stream Id
        /// </summary>
        MpcFileTableView GetByStreamId(Guid streamId);

        /// <summary>
        /// Returns New Path for directory/file to be stored 
        /// </summary>
        string GetNewPathLocator(string path, string fileTableName);
    }
}
