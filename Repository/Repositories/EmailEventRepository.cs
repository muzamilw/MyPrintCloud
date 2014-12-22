using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Email Event Repository
    /// </summary>
    public class EmailEventRepository : BaseRepository<EmailEvent>, IEmailEventRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailEventRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<EmailEvent> DbSet
        {
            get
            {
                return db.EmailEvents;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Email Events
        /// </summary>
        public override IEnumerable<EmailEvent> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
