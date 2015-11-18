using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyVoucherRedeemRepository : IBaseRepository<CompanyVoucherRedeem, long>
    {
        bool IsVoucherUsedByCustomer(long contactId, long companyId, long DiscountVoucherId);
        void AddReedem(long contactId, long companyId, long DiscountVoucherId);
        CompanyVoucherRedeem GetReedeemVoucherRecord(long contactId, long companyId, long DiscountVoucherId);
    }
}
