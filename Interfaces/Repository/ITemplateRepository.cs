using MPC.Models.Common;
using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Template Repository 
    /// </summary>
    public interface ITemplateRepository : IBaseRepository<Template, int>
    {
        int DeleteTemplate(long templateId);

        Template GetTemplate(long productID, bool loadPages);

        List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID, List<ProductCategoriesView> PCview);
        Template GetTemplate(long productID, out List<TemplatePage> listPages, out List<TemplateObject> listTemplateObjs);
        string GetTemplateNameByTemplateID(long tempID);

        long CloneTemplateByTemplateID(long TempID);
        void DeleteTemplatePagesAndObjects(long ProductID);
        void DeleteTemplatePagesAndObjects(long ProductID, out List<TemplateObject> listObjs,out List<TemplatePage> listPages);
        bool DeleteTemplate(long ProductID, out long CategoryID);
        void populateTemplateInfo(long templateID, Item ItemRecc, out Template template, out List<TemplatePage> tempPages);
        bool updateTemplate(long productID, double pdfWidth, double pdfHeight,int count);
        bool updateTemplatePages(int count, long productId);
        bool updateTemplate(long productID, double pdfWidth, double pdfHeight, List<TemplatePage> listPages);
        bool updateTemplate(long productID, double pdfWidth, double pdfHeight, List<TemplatePage> listNewPages, List<TemplatePage> listOldPages, List<TemplateObject> listObjects);
        long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, out List<TemplatePage> objPages, long OrganisationID, out List<TemplateBackgroundImage> objImages);
        long SaveTemplateLocally(Template oTemplate, List<TemplatePage> oTemplatePages, List<TemplateObject> oTemplateObjects, List<TemplateBackgroundImage> oTemplateImages, List<TemplateFont> oTemplateFonts, long organisationID, out List<TemplateFont> fontsToDownload, int mode, long localTemplateID);
        void SaveTemplate(long productID, List<TemplatePage> listPages, List<TemplateObject> listObjects);
        Template CreateTemplate(long productID, long categoryIdv2, double height, double width, long itemId);

        double getOrganisationBleedArea(long organisationID);

       
        double ConvertLength(double Input, MPC.Models.Common.LengthUnit InputUnit, MPC.Models.Common.LengthUnit OutputUnit);
    }
}
