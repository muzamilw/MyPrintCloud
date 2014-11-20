using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.IServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.Services
{
    public class MyCompanyDomainService: IMyCompanyDomainService
    {
        private readonly ICompanyDomainRepository _companyDomainRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyDomainRepository"></param>
        public MyCompanyDomainService(ICompanyDomainRepository companyDomainRepository)
        {
            this._companyDomainRepository = companyDomainRepository;
        }
        
    }
}
