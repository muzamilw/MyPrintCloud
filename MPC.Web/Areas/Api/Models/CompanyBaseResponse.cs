﻿using System;
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
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<Widget> Widgets { get; set; }
        public IEnumerable<CmsPageDropDown> CmsPageDropDownList { get; set; }
        public IEnumerable<CostCentreDropDown> CostCenterDropDownList { get; set; }
        public IEnumerable<CountryDropDown> CountryDropDowns { get; set; }
        public IEnumerable<StateDropDown> StateDropDowns { get; set; }


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
        // public IEnumerable<Department> Departments { get; set; }
        // public IEnumerable<AccountManager> AccountManagers { get; set; }
    }
}