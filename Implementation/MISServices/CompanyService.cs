using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyService: ICompanyService
    {
        #region Private

        private readonly ICompanyRepository companyRepository;
        private readonly ISystemUserRepository systemUserRepository;

        #endregion

        #region Constructor

        public CompanyService(ICompanyRepository companyRepository, ISystemUserRepository systemUserRepository)
        {
            this.companyRepository = companyRepository;
            this.systemUserRepository = systemUserRepository;
        }
        #endregion

        #region Public

        public CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request)
        {
            return companyRepository.SearchCompanies(request);
        }
        public Company GetCompanyById(int companyId)
        {
            return companyRepository.GetCompanyById(companyId);
        }

        public CompanyBaseResponse GetBaseData()
        {
            return new CompanyBaseResponse
                   {
                       SystemUsers = systemUserRepository.GetAll()
                   };
        }

        #endregion
    }
}
