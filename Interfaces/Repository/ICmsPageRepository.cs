﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.Common;

namespace MPC.Interfaces.Repository
{
    public interface ICmsPageRepository : IBaseRepository<CmsPage, long>
    {

        List<CmsPage> GetSecondaryPages(long companyId);

        /// <summary>
        /// Get CMS Pages
        /// </summary>
        SecondaryPageResponse GetCMSPages(SecondaryPageRequestModel request);
        /// <summary>
        /// Get All enabled System Pages 
        /// </summary>
        /// <returns></returns>
        List<CmsPageModel> GetSystemPagesAndSecondaryPages(long CompanyId);
    }
}
