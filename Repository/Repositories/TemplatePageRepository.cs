using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// TemplatePage Repository
    /// </summary>
    public class TemplatePageRepository : BaseRepository<TemplatePage>, ITemplatePageRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplatePageRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplatePage> DbSet
        {
            get
            {
                return db.TemplatePages;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find TemplatePage
        /// </summary>
        public TemplatePage Find(int id)
        {
            return DbSet.Find(id);
        }
        public List<TemplatePage> GetTemplatePages(long productId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.TemplatePages.Where(g => g.ProductId == productId).OrderBy(g => g.PageNo).ToList();
            return result;
        }

        #endregion

        
    }
}
