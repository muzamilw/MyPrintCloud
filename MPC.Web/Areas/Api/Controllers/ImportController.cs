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

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ImportController : ApiController
    {
        #region Private
        private readonly ICompanyService companyService;

        #endregion
        #region Constructor
        public ImportController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        #endregion
        #region Public


        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilter]
        public void Post(ImportCompanyContactCsv request)
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
                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                var filePath = Path.Combine(mapPath);
                FileHelperEngine<ImportCompanyContact> engine = new FileHelperEngine<ImportCompanyContact>();

                ImportCompanyContact[] dataLoaded = engine.ReadFile(filePath);
                //List<ImportCompanyContact> tmp = new List<ImportCompanyContact>(dataLoaded);
                //tmp.RemoveAt(0);
                //dataLoaded = tmp.ToArray();
                if (dataLoaded.Any())
                {
                    IEnumerable<StagingImportCompanyContactAddress> enumerable = dataLoaded.Select(x => x.Createfrom(request.CompanyId)).ToList();
                    //List<StagingImportCompanyContactAddress> list = enumerable as List<StagingImportCompanyContactAddress>;
                    //list.Remove(list[0]);
                    //enumerable = list;
                    companyService.SaveImportedCompanyContact(enumerable);
                }
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
            }
        }
        #endregion
    }
}