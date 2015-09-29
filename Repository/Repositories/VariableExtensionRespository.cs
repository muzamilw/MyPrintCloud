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
    public class VariableExtensionRespository : BaseRepository<VariableExtension>, IVariableExtensionRespository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public VariableExtensionRespository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<VariableExtension> DbSet
        {
            get
            {
                return db.VariableExtensions;
            }
        }

        #endregion

        #region Public
        #endregion
    }
}
