using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IWebstoreClaimsHelperService
    {
        long OrganisationId();

        long CompanyId();
        CompanyContact LoginContact();
    }
}
