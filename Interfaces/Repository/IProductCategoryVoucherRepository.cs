﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IProductCategoryVoucherRepository
    {
        bool isVoucherAppliedOnThisProductCategory(long VoucherId, long ItemId);
        List<long?> GetItemIdsListByCategoryId(long VoucherId, List<int?> ItemIds, List<long?> filteredItemIds);
    }
}
