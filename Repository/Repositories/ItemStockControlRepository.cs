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
    public class ItemStockControlRepository : BaseRepository<ItemStockControl>, IItemStockControlRepository
    {
        public ItemStockControlRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemStockControl> DbSet
        {
            get
            {
                return db.ItemStockControls;
            }
        }

        public ItemStockControl GetStockOfItemById(long itemID)
        {
            try
            {
                return db.ItemStockControls.Where(i => i.ItemId == itemID).FirstOrDefault();
            }
          catch(Exception ex)
            {
                throw ex;
            }
            
        }
     
    }
}
