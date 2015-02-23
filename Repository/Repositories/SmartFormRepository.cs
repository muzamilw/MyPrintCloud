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
    class SmartFormRepository : BaseRepository<TemplateVariable>, ISmartFormRepository
    {
          #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmartFormRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<TemplateVariable> DbSet
        {
            get
            {
                return db.TemplateVariables;
            }
        }



        #endregion
        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateVariable Find(int id)
        {
            return DbSet.Find(id);
        }
        #endregion
    }
}
