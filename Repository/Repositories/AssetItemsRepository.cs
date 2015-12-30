<<<<<<< HEAD
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
    class AssetItemsRepository : BaseRepository<AssetItem>, IAssetItemsRepository
    {
=======
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
    class AssetItemsRepository : BaseRepository<AssetItem>, IAssetItemsRepository
    {
>>>>>>> 2ec5fd5bb07087131b53d31db5e5e7306a722c5b
        public AssetItemsRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
<<<<<<< HEAD
        /// </summary>
=======
        /// </summary>
>>>>>>> 2ec5fd5bb07087131b53d31db5e5e7306a722c5b
        protected override IDbSet<AssetItem> DbSet
        {
            get
            {
                return db.AssetItems;
            }
<<<<<<< HEAD
        }

        public bool AddAssetItems(List<AssetItem> AssetItemsList)
        {
            bool result = false;
            List<AssetItem> ModelList = new List<AssetItem>();
            AssetItem Model = new AssetItem();
            foreach (var item in AssetItemsList)
            {
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
    }
}
=======
        }

        public bool AddAssetItems(List<AssetItem> AssetItemsList)
        {
            bool result = false;
            List<AssetItem> ModelList = new List<AssetItem>();
            AssetItem Model = new AssetItem();
            foreach (var item in AssetItemsList)
            {
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
    }
}
>>>>>>> 2ec5fd5bb07087131b53d31db5e5e7306a722c5b
