using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using System.Collections.Generic;
using System;

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

        public List<ItemStockOption> GetStockList(long ItemID, long companyID)
        {

            return db.ItemStockOptions.Include("StockItem").Where(i => i.ItemId == ItemID).OrderBy(g => g.OptionSequence).ToList();
           // return db.ItemStockOptions.Where(i => i.ItemId == ItemID && i.CompanyId == companyID).OrderBy(g => g.OptionSequence).ToList();

        }

        public List<ItemStockOption> GetAllStockListByItemID(long ItemID, long companyID)
        {
            try
            {
                if (companyID > 0)
                {
                    return db.ItemStockOptions.Where(i => i.ItemId == ItemID && i.CompanyId == companyID).OrderBy(g => g.OptionSequence).ToList();
                }
                else
                {
                    return db.ItemStockOptions.Where(i => i.ItemId == ItemID && i.CompanyId == null).OrderBy(g => g.OptionSequence).ToList();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
           
        }
        #endregion
    }
}
