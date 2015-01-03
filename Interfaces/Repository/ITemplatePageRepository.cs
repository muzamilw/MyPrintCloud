using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Template Page Repository 
    /// </summary>
    public interface ITemplatePageRepository : IBaseRepository<TemplatePage, int>
    {
        List<TemplatePage> GetTemplatePages(int productId);
    }
}
