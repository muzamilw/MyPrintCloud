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
    /// Widget Repository
    /// </summary>
    public class WidgetRepository : BaseRepository<Widget>, IWidgetRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WidgetRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Widget> DbSet
        {
            get
            {
                return db.Widgets;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Widget
        /// </summary>
        public override IEnumerable<Widget> GetAll()
        {
            return DbSet.Where(o => o.OrganisationId == null).ToList();
        }
        public IEnumerable<Widget> GetWidgetsByOrganisation()
        {
            return DbSet.Where(o => o.OrganisationId == OrganisationId).ToList();
        }
        public Widget GetWidgetById(long widgetId)
        {
            return DbSet.FirstOrDefault(o => o.WidgetId == widgetId);
        }
        #endregion
    }
}
