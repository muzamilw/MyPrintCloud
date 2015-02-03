using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyContactService
    {
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="companyContact"></param>
        /// <returns></returns>
        CompanyContact Save(CompanyContact companyContact);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="companyContactId"></param>
        /// <returns></returns>
        bool Delete(long companyContactId);
    }
}
