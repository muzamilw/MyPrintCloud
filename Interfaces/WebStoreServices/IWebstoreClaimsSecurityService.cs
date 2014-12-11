using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IWebstoreClaimsSecurityService
    {
        void AddClaimsToIdentity(long companyIdentity, ClaimsIdentity claimsIdentity);
    }
}
