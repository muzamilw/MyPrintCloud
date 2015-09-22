using MPC.Interfaces.MISServices;
using MPC.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.WebBase.Mvc;
using MPC.MIS.Areas.Api.Models;
using System.Net;
using MPC.Interfaces.Data;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyVariableIconController :  ApiController
    {
         private readonly ICompanyService companyService;

         public CompanyVariableIconController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

         public Models.RealEstateVariableIconeListViewResponse Get([FromUri]CompanyVariableIconRequestModel request)
         {
             return companyService.GetCompanyVariableIcons(request).CreateFromListView();
         }

         /// <summary>
         /// Add/Update Company
         /// </summary>
         [ApiException]
         [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
         [CompressFilterAttribute]
         public void Post(CompanyVariableIconRequestModel request)
         {
             if (!ModelState.IsValid)
             {
                 throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
             }


             companyService.SaveCompanyVariableIcon(request);
         }

         [ApiException]
         public void Delete(CompanyVariableIconeDeleteModel variableIcon)
         {
             if (variableIcon.VariableIconeId == 0 || !ModelState.IsValid)
             {
                 throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
             }
             companyService.DeleteCompanyVariableIcon(variableIcon.VariableIconeId);
         }
    }
}