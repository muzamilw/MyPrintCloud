using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class CrmSupplierService: ICrmSupplierService
    {
        #region Private
        private readonly ICompanyRepository companyRepository;
        #endregion

        #region Constructor

        public CrmSupplierService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }
        #endregion

        #region Public 
        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            return companyRepository.SearchCompanies(request);
        }
        #endregion
    }
}
