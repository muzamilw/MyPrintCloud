using System;
using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// Save Company
        /// </summary>
        private Company SaveNewCompany(Company company)
        {
            companyRepository.Add(company);
            companyRepository.SaveChanges();
            return company;
        }

        /// <summary>
        /// Update Company
        /// </summary>
        private Company UpdateCompany(Company company)
        {
            companyRepository.Update(company);
            companyRepository.SaveChanges();
            return company;
        }
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
        public void SaveFile(string filePath, long companyId)
        {
            Company company= companyRepository.GetCompanyById(companyId);
            if (company.Image!= null)
            {
                if (File.Exists(company.Image))
                {
                    //If already organisation logo is save,it delete it 
                    File.Delete(company.Image);
                }
            }
            company.Image = filePath;
            companyRepository.SaveChanges();
        }

        public Company SaveCompany(Company company)
        {
            Company companyDbVersion = companyRepository.Find(company.CompanyId);
            if (companyDbVersion == null)
            {
                SaveNewCompany(company);
            }
            else
            {
                UpdateCompany(company);
            }
            return null;
        }

        public long GetOrganisationId()
        {
            return companyRepository.OrganisationId;
        }

        #endregion
    }
}
