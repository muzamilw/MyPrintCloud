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
        List<TemplateColorStyle> GetColorStyle(long ProductId, long CustomerID);
        List<TemplateColorStyle> GetColorStyle(long ProductId);
        string SaveCorpColor(int C, int M, int Y, int K, string Name, long CustomerID);
        string UpdateCorpColor(long id, string type);
    }
}
