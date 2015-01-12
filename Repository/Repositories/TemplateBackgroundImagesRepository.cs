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
    public class TemplateBackgroundImagesRepository : BaseRepository<TemplateBackgroundImage>, ITemplateBackgroundImagesRepository
    {
              #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateBackgroundImagesRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateBackgroundImage> DbSet
        {
            get
            {
                return db.TemplateBackgroundImages;
            }
        }
        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateBackgroundImage Find(int id)
        {
            return DbSet.Find(id);
        }
   
        public void DeleteTemplateBackgroundImages(long productID)
        {

        }

        #endregion
    }
}
