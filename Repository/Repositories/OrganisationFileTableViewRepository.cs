using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Organisation File Table View Repository
    /// </summary>
    public class OrganisationFileTableViewRepository : BaseRepository<OrganisationFileTableView>, IOrganisationFileTableViewRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrganisationFileTableViewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<OrganisationFileTableView> DbSet
        {
            get
            {
                return db.OrganisationFileTableViews;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find File in File Table
        /// </summary>
        public OrganisationFileTableView Find(Guid id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get File by StreamId
        /// </summary>
        public OrganisationFileTableView GetByStreamId(Guid streamId)
        {
            return DbSet.FirstOrDefault(file => file.StreamId == streamId);
        }

        /// <summary>
        /// Returns new path for directory/file
        /// </summary>
        public string GetNewPathLocator(string path, string fileTableName)
        {
            return db.GetNewPathLocator(path, fileTableName);
        }

        #endregion
    }
}
