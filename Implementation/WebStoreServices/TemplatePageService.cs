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
        // get template pages called from designer //// added by saqib ali
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
        // get template pages, called from webstore // added by saqib ali
        public List<TemplatePage> GetTemplatePagesSP(long productId)
        {
            var list = _templatePageRepository.GetTemplatePages(productId);

            return list;
        }

        // called from mis to create blank background pdfs of only two pages in template // added by saqib ali
        public bool CreateBlankBackgroundPDFs(long TemplateID, double height, double width, int Orientation, long OrganisationID)
        {
         
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
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
                    throw new MPCException(ex.ToString(), OrganisationID);
                }
                finally
                {
                    theDoc.Clear();
                }
                
            }
            return true;

        }

        // called from MIS to create blank pdf files based on list of pages send// added by saqib ali
        public bool CreateBlankBackgroundPDFsByPages(long TemplateID, double height, double width, int Orientation, List<TemplatePage> PagesList, long OrganisationID)
        {
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
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
                            theDoc.Save(basePath + "Side" + PagesList[i].PageNo.ToString() + ".pdf");
                            if (File.Exists(basePath + "templatImgBk" + (PagesList[i].PageNo).ToString() + ".jpg"))
                            {
                                File.Delete(basePath + "templatImgBk" + (PagesList[i].PageNo).ToString() + ".jpg");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new MPCException(ex.ToString(), OrganisationID);
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

        // called from MIS to create blank pdf of the given template page// added by saqib ali
        public string CreatePageBlankBackgroundPDFs(long TemplateID, TemplatePage oPage, double height, double width, long OrganisationID)
        {
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
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
                    theDoc.Save(basePath + "Side" + oPage.PageNo.ToString() + ".pdf");
                    if (File.Exists(basePath + "templatImgBk" + oPage.PageNo.ToString() + ".jpg"))
                    {
                        File.Delete(basePath + "templatImgBk" + oPage.PageNo.ToString() + ".jpg");
                    }
                }
                catch (Exception ex)
                {
                    throw new MPCException(ex.ToString(), OrganisationID);
                }
                finally
                {
                    theDoc.Clear();
                }
                return TemplateID.ToString() + "/" + oPage.PageName + oPage.PageNo.ToString() + ".pdf";
            }
        }

        // called from MIS to delete background files of the given template pages  // added by saqib ali
        public bool DeleteBlankBackgroundPDFsByPages(long TemplateID, List<TemplatePage> PagesList, long OrganisationID)
        {

            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
            string basePath = drURL + TemplateID.ToString() + "/";
            try
            {
                if (Directory.Exists(basePath))
                {
                    foreach (var objPage in PagesList)
                    {
                        //if (File.Exists(basePath + "Side" + objPage.PageNo.ToString() + ".pdf"))  // commented becasue we have page name in page list
                        //{
                        //    File.Delete(basePath + "Side" + objPage.PageNo.ToString() + ".pdf");
                        //}
                        if (File.Exists(drURL + objPage.BackgroundFileName.ToString())) 
                        {
                            File.Delete(drURL + objPage.BackgroundFileName.ToString());
                        }
                        if (File.Exists(basePath + "templatImgBk" + objPage.PageNo.ToString() + ".jpg"))
                        {
                            File.Delete(basePath + "templatImgBk" + objPage.PageNo.ToString() + ".jpg");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
               throw new MPCException(ex.ToString(), OrganisationID);
            }
        }
        #endregion
    }
}
