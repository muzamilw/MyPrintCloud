﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyBannerSetRepository : IBaseRepository<CompanyBannerSet, long>
    {
        List<CompanyBannerSet> GetCompanyBannersById(long companyId, long organisationId);
    }
}