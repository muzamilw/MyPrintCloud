using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyContactService
    {
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="companyContactId"></param>
        /// <returns></returns>
        bool Delete(long companyContactId);
    }
}
