using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ITemplateColorStylesRepository : IBaseRepository<TemplateColorStyle, long>
    {
        List<TemplateColorStyle> GetColorStyle(int ProductId, int CustomerId);
        List<TemplateColorStyle> GetColorStyle(int ProductId);
        int SaveCorpColor(int C, int M, int Y, int K, string Name, int CustomerID);
        string UpdateCorpColor(int id, string type);
    }
}
