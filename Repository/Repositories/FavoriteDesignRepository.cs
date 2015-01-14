﻿using Microsoft.Practices.Unity;
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
    public class FavoriteDesignRepository : BaseRepository<FavoriteDesign>, IFavoriteDesignRepository
    {
          #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FavoriteDesignRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<FavoriteDesign> DbSet
        {
            get
            {
                return db.FavoriteDesigns;
            }
        }

        public  FavoriteDesign GetFavContactDesign(long templateID, long contactID)
        {
            try
            {
                return db.FavoriteDesigns.Where(a => a.ContactUserId == contactID && a.TemplateId == templateID).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}