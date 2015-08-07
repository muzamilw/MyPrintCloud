using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyVoucherRedeemRepository
    {
        bool IsVoucherUserByCustomer(long contactId, long companyId, long DiscountVoucherId);
        void AddReedem(long contactId, long companyId, long DiscountVoucherId);
    }
}
