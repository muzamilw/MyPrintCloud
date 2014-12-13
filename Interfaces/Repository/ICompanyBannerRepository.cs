using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using System.Web;
namespace MPC.Interfaces.Repository
{
    public interface ICompanyBannerRepository : IBaseRepository<CompanyBanner, long>
    {
       
        List<CompanyBanner> GetCompanyBannersById(long companyId);
    }
}
