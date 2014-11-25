using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICompanyBannerSetService 
    {
        List<CompanyBannerSet> GetCompanyBannersById(long companyId, long organisationId);
    }
}
