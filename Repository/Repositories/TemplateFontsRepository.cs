using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class TemplateFontsRepository : BaseRepository<TemplateFont>, ITemplateFontsRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateFontsRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateFont> DbSet
        {
            get
            {
                return db.TemplateFonts;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateFont Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        ///  Get template object by template id 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<TemplateFont> GetFontList(int productId, int customerId)
        {
           // db.Configuration.LazyLoadingEnabled = true;
            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateFont> lFont = new List<TemplateFont>();
           // lFont = db.sp_GetUsedFontsUpdated(productId, customerId).ToList();
            return lFont;

        }

        #endregion
    }
}
