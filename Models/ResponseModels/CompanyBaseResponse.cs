using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CompanyBaseResponse
    {
        /// <summary>
        /// System Users List
        /// </summary>
        public IEnumerable<SystemUser> SystemUsers { get; set; }
        public IEnumerable<CompanyTerritory> CompanyTerritories { get; set; }
        public IEnumerable<CompanyContactRole> CompanyContactRoles { get; set; }
        public IEnumerable<PageCategory> PageCategories { get; set; }
        public IEnumerable<RegistrationQuestion> RegistrationQuestions { get; set; }
        public IEnumerable<EmailEvent> EmailEvents { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<SectionFlag> SectionFlags { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<Widget> Widgets { get; set; }
        public IEnumerable<CmsPage> CmsPages { get; set; }
        public IEnumerable<CostCentre> CostCentres { get; set; }
        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<State> States { get; set; }
        public IEnumerable<FieldVariable> FieldVariablesForSmartForm { get; set; }
        public IEnumerable<FieldVariable> SystemVariablesForSmartForms { get; set; }

        public FieldVariableResponse FieldVariableResponse { get; set; }
        public SmartFormResponse SmartFormResponse { get; set; }
        public DiscountVoucherListViewResponse DiscountVoucherListViewResponse { get; set; }
        public IEnumerable<SectionFlag> PriceFlags { get; set; }
        
        /// <summary>
        /// Organisation Id
        /// </summary>
        public long? OrganisationId { get; set; }

        public string  Currency { get; set; }

        // public IEnumerable<Department> Departments { get; set; }
        // public IEnumerable<AccountManager> AccountManagers { get; set; }
    }
}
