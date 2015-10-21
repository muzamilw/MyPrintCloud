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
    public class ItemsVoucherRepository : BaseRepository<ItemsVoucher>, IItemsVoucherRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemsVoucherRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemsVoucher> DbSet
        {
            get
            {
                return db.ItemsVouchers;
            }
        }

        public bool isVoucherAppliedOnThisProduct(long VoucherId, long ItemId) 
        {
            ItemsVoucher vItem = db.ItemsVouchers.Where(v => v.ItemId == ItemId && v.VoucherId == VoucherId).FirstOrDefault();
            if(vItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<long?> GetListOfItemIdsByVoucherId(long VoucherId, List<int?> ItemIds)
        {
            List<ItemsVoucher> vItem = db.ItemsVouchers.Where(v => ItemIds.Contains((int)v.ItemId) && v.VoucherId == VoucherId).ToList();
            if (vItem != null && vItem.Count() > 0)
            {
                return vItem.Select(i => i.ItemId).ToList();
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
