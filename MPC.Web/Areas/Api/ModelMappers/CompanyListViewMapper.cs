using System.IO;
using System.Web;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyListViewMapper
    {
        public static ApiModels.CompanyListViewModel ListViewModelCreateFrom(this DomainModels.Company source)
        {
            byte[] bytes = null;
            string imagePath = HttpContext.Current.Server.MapPath("~/" + source.Image);
            if (source.Image != null && File.Exists(imagePath))
            {
                bytes = source.Image != null ? File.ReadAllBytes(imagePath) : null;
            }
            return new ApiModels.CompanyListViewModel
            {
                AccountNumber = source.AccountNumber,
                CompanyId = source.CompanyId,
                IsCustomer = source.IsCustomer,
                Name = source.Name,
                Status = source.Status,
                URL = source.URL,
                Image = bytes
            };
        }
    }
}