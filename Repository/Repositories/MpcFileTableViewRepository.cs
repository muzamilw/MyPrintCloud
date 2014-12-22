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
    /// Get Items List View Repository
    /// </summary>
    public class MpcFileTableViewRepository : BaseRepository<MpcFileTableView>, IMpcFileTableViewRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MpcFileTableViewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<MpcFileTableView> DbSet
        {
            get
            {
                return db.MpcFileTableViews;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find File in File Table
        /// </summary>
        public MpcFileTableView Find(Guid id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get File by StreamId
        /// </summary>
        public MpcFileTableView GetByStreamId(Guid streamId)
        {
            return DbSet.FirstOrDefault(file => file.StreamId == streamId);
        }

        #endregion
    }
}
