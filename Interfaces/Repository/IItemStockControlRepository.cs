﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
namespace MPC.Interfaces.Repository
{
    public interface IItemStockControlRepository : IBaseRepository<ItemStockControl, long>
    {
        ItemStockControl GetStockOfItemById(long itemID);
    }
}
