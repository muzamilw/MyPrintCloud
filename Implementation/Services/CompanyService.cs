
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  MPC.Interfaces;
using MPC.Interfaces.IServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;


namespace MPC.Implementation.Services
{
    public class CompanyService : ICompanyService   
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ICompanyRepository companyRepository;
     

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository )
        {
            this.companyRepository = companyRepository;
         
        }

        #endregion


        #region Public
        /// <summary>
        /// Resolves the Company/Store by the domain provided
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Company GetCompanyByDomain(string domain)
        {
            return companyRepository.GetCompanyByDomain(domain);
        }

        #endregion
    }
}
