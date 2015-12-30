<<<<<<< HEAD
﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IAssetItemsRepository : IBaseRepository<AssetItem, long>
    {
        bool AddAssetItems(List<AssetItem> AssetItemsList);
    }
}
=======
﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IAssetItemsRepository : IBaseRepository<AssetItem, long>
    {
        bool AddAssetItems(List<AssetItem> AssetItemsList);
    }
}
>>>>>>> 2ec5fd5bb07087131b53d31db5e5e7306a722c5b
