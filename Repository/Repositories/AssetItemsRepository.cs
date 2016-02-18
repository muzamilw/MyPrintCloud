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
    class AssetItemsRepository : BaseRepository<AssetItem>, IAssetItemsRepository
    {
        public AssetItemsRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AssetItem> DbSet
        {
            get
            {
                return db.AssetItems;
            }
        }

        public bool AddAssetItems(List<AssetItem> AssetItemsList)
        {
            bool result = false;
            List<AssetItem> ModelList = new List<AssetItem>();
           
            foreach (var item in AssetItemsList)
            {
                AssetItem Model = new AssetItem();
                Model.AssetId = item.AssetId;
                Model.FileUrl = item.FileUrl;
                db.AssetItems.Add(Model);
            }

            if (db.SaveChanges() > 0)
            {
                result = true;
            }
            return result;
        }
        public List<AssetItem> GetAssetItemsByAssetID(long AssetID)
        {
            return db.AssetItems.Where(i => i.AssetId == AssetID).ToList();
        }
        public void RemoveAssetItem(long AssetID)
        {
          AssetItem item=  db.AssetItems.Where(i => i.AssetId == AssetID).FirstOrDefault();
          db.AssetItems.Remove(item);
          db.SaveChanges();
        }
        public void RemoveAssetItems(List<AssetItem> RemoveAssetItemsIDs)
        {
            foreach (var item in RemoveAssetItemsIDs)
            {
                AssetItem assetsItems = db.AssetItems.Where(i => i.AssetItemId == item.AssetItemId).FirstOrDefault();
                db.AssetItems.Remove(assetsItems);
            }
            db.SaveChanges();
        }
        public string AssetItemFilePath(long AssetItemId)
        {
            string url = string.Empty;
            AssetItem item= db.AssetItems.Where(i => i.AssetItemId == AssetItemId).FirstOrDefault();
            url = item.FileUrl;
            return url;
        }
    }
}
