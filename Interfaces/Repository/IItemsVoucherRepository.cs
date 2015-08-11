using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IItemsVoucherRepository
    {
        bool isVoucherAppliedOnThisProduct(long VoucherId, long ItemId);
        bool isVoucherAppliedOnListOfCartProduct(long VoucherId, List<int?> ItemIds);
    }
}
