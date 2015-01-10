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

        List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID, List<ProductCategoriesView> PCview);

        string GetTemplateNameByTemplateID(int tempID);

        int CloneTemplateByTemplateID(int TempID);
        void DeleteTemplatePagesAndObjects(long ProductID);
        bool DeleteTemplate(long ProductID, out long CategoryID);
        long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, out List<TemplatePage> objPages, long organizationID, out List<TemplateBackgroundImage> objImages);
    }
}
