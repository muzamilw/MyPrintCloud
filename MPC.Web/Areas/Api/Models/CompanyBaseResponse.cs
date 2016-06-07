using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyBaseResponse
    {
        /// <summary>
        /// System Users List
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }
        public IEnumerable<CompanyTerritory> CompanyTerritories { get; set; }
        public IEnumerable<CompanyContactRole> CompanyContactRoles { get; set; }
        public IEnumerable<PageCategoryDropDown> PageCategories { get; set; }
        public IEnumerable<RegistrationQuestionDropDown> RegistrationQuestions { get; set; }
        public IEnumerable<EmailEvent> EmailEvents { get; set; }
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<Widget> Widgets { get; set; }
        public IEnumerable<CmsPageDropDown> CmsPageDropDownList { get; set; }
        public IEnumerable<CostCentreDropDown> CostCenterDropDownList { get; set; }
        public IEnumerable<CountryDropDown> Countries { get; set; }
        public IEnumerable<StateDropDown> States { get; set; }
        public IEnumerable<CountryDropDown> CountryDropDowns { get; set; }
        public IEnumerable<StateDropDown> StateDropDowns { get; set; }
        public FieldVariableResponse FieldVariableResponse { get; set; }
        public SmartFormResponse SmartFormResponse { get; set; }
        public IEnumerable<FieldVariableForSmartForm> FieldVariableForSmartForms { get; set; }
        public IEnumerable<SkinForTheme> Themes { get; set; }
        public IEnumerable<FieldVariableForSmartForm> SystemVariablesForSmartForms { get; set; }
        public DiscountVoucherListViewResponse DiscountVoucherListViewResponse { get; set; }

        /// <summary>
        /// Default Sprite Image
        /// </summary>
        public byte[] DefaultSpriteImage { get; set; }

        /// <summary>
        /// Default Sprite Image Source
        /// </summary>
        public string DefaultSpriteImageSource
        {
            get
            {
                if (DefaultSpriteImage == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(DefaultSpriteImage);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        /// <summary>
        /// Default Company Css
        /// </summary>
        public string DefaultCompanyCss { get; set; }

        /// <summary>
        /// Organisation Id
        /// </summary>
        public long? OrganisationId { get; set; }

        /// <summary>
        /// For Public store use Retail Store Name Key that exist in web config
        /// </summary>
        public string RetailStoreNameWebConfigValue { get; set; }

        /// <summary>
        /// For Private store use Corporate Store Name Key that exist in web config
        /// </summary>
        public string CorporateStoreNameWebConfigValue { get; set; }
        // public IEnumerable<Department> Departments { get; set; }
        // public IEnumerable<AccountManager> AccountManagers { get; set; }

        public IEnumerable<SectionFlagDropDown> PriceFlags { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }
        public long? DefaultCountryId { get; set; }
        public string OrganisationName { get; set; }
        public Guid LoginUserId { get; set; }
        public string LoginUserFullName { get; set; }
        public string LoginUserName { get; set; }
    }
}