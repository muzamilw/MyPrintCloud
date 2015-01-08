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
        Template GetTemplate(int productID);

        List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID);

        string GetTemplateNameByTemplateID(int tempID);

        ProductCategoriesView GetMappedCategory(string CatName, int CID);

        int CloneTemplateByTemplateID(int TempID);
    }
}
