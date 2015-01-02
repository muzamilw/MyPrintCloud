using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplatePageService : ITemplatePageService
    {
         #region private
        public readonly ITemplatePageRepository _templatePageRepository;
        #endregion
        #region constructor
        public TemplatePageService(ITemplatePageRepository templatePageRepository)
        {
            this._templatePageRepository = templatePageRepository;
        }
        #endregion

        #region public
        public List<TemplatePage> GetTemplatePages(int productId)
        {
            var list = _templatePageRepository.GetTemplatePages(productId);

            //foreach (var objPage in list)
            //{
            //    string targetFolder = "";
            //    targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
            //    if (objPage.BackGroundType != 3)
            //    {
            //        if (File.Exists(targetFolder + TemplateID + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg"))
            //        {
            //            objPage.BackgroundFileName = "Designer/Products/" + TemplateID + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg";
            //        }
            //        else
            //        {
            //            objPage.BackgroundFileName = "";
            //        }
            //    }
            //}
            return list;
        }
        #endregion
    }
}
