using System.Linq;
﻿using System.Linq;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyListViewMapper
    {
        public static ApiModels.CompanyListViewModel ListViewModelCreateFrom(this DomainModels.Company source)
        {

            return new ApiModels.CompanyListViewModel
            {
                AccountNumber = source.AccountNumber,
                CompanyId = source.CompanyId,
                IsCustomer = source.IsCustomer,
                Name = source.Name,
                Status = source.Status,
                URL = source.URL,
                ImageBytes = source.Image,
                IsStoreLive = source.isStoreLive,
                DefaultDomain = source.CompanyDomains != null ? source.CompanyDomains.FirstOrDefault().Domain: string.Empty
            };
        }
    }
}