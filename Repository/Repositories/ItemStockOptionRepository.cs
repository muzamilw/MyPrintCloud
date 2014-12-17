﻿using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemStockOption Repository
    /// </summary>
    public class ItemStockOptionRepository : BaseRepository<ItemStockOption>, IItemStockOptionRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemStockOptionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemStockOption> DbSet
        {
            get
            {
                return db.ItemStockOptions;
            }
        }

        #endregion

        #region public
        #endregion
    }
}