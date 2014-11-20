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
    /// Markup Repository
    /// </summary>
    public class MarkupRepository : BaseRepository<Markup>, IMarkupRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarkupRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Markup> DbSet
        {
            get
            {
                return db.Markups;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All MarkUp for User Domain Key
        /// </summary>
        public override IEnumerable<Markup> GetAll()
        {
            return DbSet.Where(markup => markup.UserDomainKey == UserDomainKey).ToList();
        }
        #endregion
    }
}
