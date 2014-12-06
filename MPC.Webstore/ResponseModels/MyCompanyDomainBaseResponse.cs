﻿
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ResponseModels
{
    public class MyCompanyDomainBaseResponse
    {
        /// <summary>
        /// Store
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Store Widget list
        /// </summary>
        public List<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }

        /// <summary>
        /// Store banner list
        /// </summary>
        public List<CompanyBanner> Banners { get; set; }

        /// <summary>
        /// All System pages and secondary pages of stores
        /// </summary>
        public List<CmsPage> SystemPages { get; set; }

        /// <summary>
        /// All secondary pages of stores
        /// </summary>
        public List<CmsPage> SecondaryPages { get; set; }

        /// <summary>
        ///  Page Categories of secondary pages 
        /// </summary>
        public List<PageCategory> PageCategories { get; set; }

        /// <summary>
        ///  Currency of Store 
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        ///  Language of Store 
        /// </summary>
        public string Language { get; set; }
    }
}