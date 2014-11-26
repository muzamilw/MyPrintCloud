
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ResponseModels
{
    public class MyCompanyDomainBaseResponse
    {
        public Company Company { get; set; }

        /// <summary>
        /// Tax Rate List
        /// </summary>
        public List<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }

        /// <summary>
        /// Markup List
        /// </summary>
        public List<CompanyBanner> Banners { get; set; }
    }
}