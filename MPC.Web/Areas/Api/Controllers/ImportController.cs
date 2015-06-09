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
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ImportController : ApiController
    {
        #region Private
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor
        public ImportController(ICompanyContactService companyContactService)
        {
            this.companyContactService = companyContactService;
        }
        #endregion
        #region Public
         [System.Web.Http.HttpGet]
        public void CompanyContact()
        {
            string mapPath = HostingEnvironment.MapPath("~/Resources/CompanyContacts.csv");
            if (mapPath != null)
            {
                var filePath = Path.Combine(mapPath);

                FileHelperEngine<ImportCompanyContact> engine = new FileHelperEngine<ImportCompanyContact>();

                ImportCompanyContact[] dataLoaded = engine.ReadFile(filePath);
                if (dataLoaded.Any())
                {
                    //IEnumerable<ReferringDoctor> enumerable = dataLoaded.Select(x => x.CreateFrom()).ToList();
                    //foreach (ReferringDoctor company in enumerable)
                    //{
                    //    DoctorRepository.Insert(company);
                    //}
                    //Cruder._uow.Save();
                }
            }
        }
         [ApiException]
         [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
         [CompressFilter]
        public void Post(ImportCompanyContactCsv request)
         {
             string base64 = request.FileBytes.Substring(request.FileBytes.IndexOf(',') + 1);
             base64 = base64.Trim('\0');
             byte[] data = Convert.FromBase64String(base64);
             string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/SampleCsv");
             if (directoryPath != null && !Directory.Exists(directoryPath)){
                 Directory.CreateDirectory(directoryPath);
             }
             string savePath = directoryPath + "\\" + "_companyContact.csv";
             File.WriteAllBytes(savePath, data);
             int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
             savePath = savePath.Substring(indexOf, savePath.Length - indexOf);

             string mapPath = HostingEnvironment.MapPath("~/"+savePath);
             if (mapPath != null)
             {
                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                 var filePath = Path.Combine(mapPath);
                 FileHelperEngine<ImportCompanyContact> engine = new FileHelperEngine<ImportCompanyContact>();

                 ImportCompanyContact[] dataLoaded = engine.ReadFile(filePath);
                 if (dataLoaded.Any())
                 {
                     IEnumerable<CompanyContact> enumerable = dataLoaded.Select(x => x.Createfrom()).ToList();
                     companyContactService.SaveImportedContact(enumerable.Select(x=>x.Createfrom()));
                 }
             }
             if (File.Exists(savePath))
             {
                 File.Delete(savePath);
             }
         }
        #endregion
    }
}