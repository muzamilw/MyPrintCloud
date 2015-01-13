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
        Template GetTemplate(long productID);
        Template GetTemplate(long productID, out List<TemplatePage> listPages,out List<TemplateObject> listTemplateObjs);
        List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID, List<ProductCategoriesView> PCview);

        string GetTemplateNameByTemplateID(int tempID);

        long CloneTemplateByTemplateID(long TempID);
        void DeleteTemplatePagesAndObjects(long ProductID);
        void DeleteTemplatePagesAndObjects(long ProductID, out List<TemplateObject> listObjs,out List<TemplatePage> listPages);
        bool DeleteTemplate(long ProductID, out long CategoryID);
        bool updateTemplate(long productID, double pdfWidth, double pdfHeight, List<TemplatePage> listPages);
        bool updateTemplate(long productID, double pdfWidth, double pdfHeight, List<TemplatePage> listNewPages, List<TemplatePage> listOldPages, List<TemplateObject> listObjects);
        long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, out List<TemplatePage> objPages, long organizationID, out List<TemplateBackgroundImage> objImages);
    }
}
