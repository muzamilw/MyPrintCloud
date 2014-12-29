using System;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Get Organisation File Table View Repository 
    /// </summary>
    public interface IOrganisationFileTableViewRepository : IBaseRepository<OrganisationFileTableView, Guid>
    {
        /// <summary>
        /// Get File by Stream Id
        /// </summary>
        OrganisationFileTableView GetByStreamId(Guid streamId);

        /// <summary>
        /// Returns New Path for directory/file to be stored 
        /// </summary>
        string GetNewPathLocator(string path, string fileTableName);
    }
}
