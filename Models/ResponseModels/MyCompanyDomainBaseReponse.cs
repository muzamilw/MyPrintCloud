using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class MyCompanyDomainBaseReponse
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
