using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using FileHelpers;
using GrapeCity.ActiveReports.PageReportModel;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.WebBase.Mvc;
using System.Net;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ExportCRMContactsController : ApiController
    {

          #region Private
        private readonly ICompanyContactService companyContactService;
        private readonly ICompanyService companyService;

        #endregion
        #region Constructor
        public ExportCRMContactsController(ICompanyContactService CompanyContactService, ICompanyService companyService)
        {
            this.companyContactService = CompanyContactService;
            this.companyService = companyService;
        }
        #endregion



        public string Get(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            long? companyId = id > 0 ? id : (long?)null;

            return companyContactService.ExportCRMContacts();
            // return itemService.GetProductCategoriesForCompany(companyId).CreateFrom();
        }
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
        [CompressFilter]
        public void Post(ImportCompanyContactCsv request)
        {
            try
            {
                string base64 = request.FileBytes.Substring(request.FileBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);
                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/SampleCsv");
                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\" + "_companyContact.csv";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);

                string mapPath = HostingEnvironment.MapPath("~/" + savePath);
                if (mapPath != null)
                {
                    // ReSharper disable once PossiblyMistakenUseOfParamsMethods
                    var filePath = Path.Combine(mapPath);
                    FileHelperEngine<ImportCRMCompanyContact> engine = new FileHelperEngine<ImportCRMCompanyContact>();

                    ImportCRMCompanyContact[] dataLoaded = engine.ReadFile(filePath);
                    //List<ImportCompanyContact> tmp = new List<ImportCompanyContact>(dataLoaded);
                    //tmp.RemoveAt(0);
                    //dataLoaded = tmp.ToArray();
                    if (dataLoaded.Any())
                    {
                        IEnumerable<StagingImportCompanyContactAddress> enumerable = dataLoaded.Select(x => x.Createfrom(request.CompanyId)).ToList();
                        //List<StagingImportCompanyContactAddress> list = enumerable as List<StagingImportCompanyContactAddress>;
                        //list.Remove(list[0]);
                        //enumerable = list;
                        companyService.SaveCRMImportedCompanyContact(enumerable);
                    }
                    if (File.Exists(savePath))
                    {
                        File.Delete(savePath);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

    }
}
