using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf8;

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
        public List<TemplatePage> GetTemplatePages(long productId)
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
        public List<TemplatePage> GetTemplatePagesSP(long productId)
        {
            var list = _templatePageRepository.GetTemplatePages(productId);

            return list;
        }

        // called from mis to create blank background pdfs of only two pages in template 
        public bool CreateBlankBackgroundPDFs(long TemplateID, double height, double width, int Orientation, long organizationID)
        {
         
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/");
            using (Doc theDoc = new Doc())
            {
                try
                {
                    string basePath = drURL + TemplateID.ToString() + "/";
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    if (Orientation == 1)  //horizontal
                    {

                        theDoc.MediaBox.Height = height;
                        theDoc.MediaBox.Width = width;
                    }
                    else
                    {
                        theDoc.MediaBox.Height = width;
                        theDoc.MediaBox.Width = height;

                    }
                    theDoc.Save(basePath + "Side1.pdf");
                    File.Copy(basePath + "Side1.pdf", basePath + "Side2.pdf", true);
                }
                catch (Exception ex)
                {
                    throw new MPCException(ex.ToString(), organizationID);
                }
                finally
                {
                    theDoc.Clear();
                }
                
            }
            return true;

        }

        // called from MIS to create blank pdf files based on list of pages send
        public bool CreateBlankBackgroundPDFsByPages(long TemplateID, double height, double width, int Orientation, List<TemplatePage> PagesList, long organizationID)
        {
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/");
            string basePath = drURL + TemplateID.ToString() + "/";
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            if (PagesList.Count > 0)
            {
                for (int i = 0; i <= PagesList.Count - 1; i++)
                {
                    using (Doc theDoc = new Doc())
                    {
                        try
                        {
                            if (PagesList[i].Orientation == 1)  //horizontal
                            {
                                theDoc.MediaBox.Height = height;
                                theDoc.MediaBox.Width = width;
                            }
                            else
                            {
                                theDoc.MediaBox.Height = width;
                                theDoc.MediaBox.Width = height;
                            }
                            theDoc.Save(basePath + "Side" + (i + 1).ToString() + ".pdf");
                        }
                        catch (Exception ex)
                        {
                            throw new MPCException(ex.ToString(), organizationID);
                        }
                        finally
                        {
                            theDoc.Clear();
                        }
                    }
                }
            }
            return true;
        }

        // called from MIS to create blank pdf of the given template page
        public string CreatePageBlankBackgroundPDFs(long TemplateID, TemplatePage oPage, double height, double width, long organizationID)
        {
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/");
            string basePath =drURL  + TemplateID.ToString() + "/";
            using (Doc theDoc = new Doc())
            {
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);

                    if (oPage.Orientation == 1)  //horizontal
                    {

                        theDoc.MediaBox.Height = height;
                        theDoc.MediaBox.Width = width;
                    }
                    else
                    {
                        theDoc.MediaBox.Height = width;
                        theDoc.MediaBox.Width = height;

                    }
                    theDoc.Save(basePath + oPage.PageName + oPage.PageNo.ToString() + ".pdf");
                }
                catch (Exception ex)
                {
                    throw new MPCException(ex.ToString(), organizationID);
                }
                finally
                {
                    theDoc.Clear();
                }
                return TemplateID.ToString() + "/" + oPage.PageName + oPage.PageNo.ToString() + ".pdf";
            }
        }
        #endregion
    }
}
