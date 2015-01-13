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

        string GetTemplateNameByTemplateID(long tempID);

        int CloneTemplateByTemplateID(int TempID);

        bool DeleteTemplate(long ProductID, out long CategoryID);
        /// <summary>
        /// To populate the template information base on template id and item rec by zohaib 10/1/2015
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="ItemRecc"></param>
        /// <param name="template"></param>
        /// <param name="tempPages"></param>
        void populateTemplateInfo(long templateID, Item ItemRecc, out Template template, out List<TemplatePage> tempPages);
    }
}
