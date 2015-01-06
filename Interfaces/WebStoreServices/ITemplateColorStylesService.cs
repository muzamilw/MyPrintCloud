using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplateColorStylesService
    {
        List<TemplateColorStyle> GetColorStyle(int ProductId, int CustomerID);
        List<TemplateColorStyle> GetColorStyle(int ProductId);
        int SaveCorpColor(int C, int M, int Y, int K, string Name, int CustomerID);
        string UpdateCorpColor(int id, string type);
    }
}
