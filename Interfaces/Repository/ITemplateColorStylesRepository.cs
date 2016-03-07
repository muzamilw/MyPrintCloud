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
        List<TemplateColorStyle> GetColorStyle(long ProductId, long CustomerId, long territoryId);
        List<TemplateColorStyle> GetColorStyle(long ProductId);
        string SaveCorpColor(int C, int M, int Y, int K, string Name, long CustomerID);
        string UpdateCorpColor(long id, string type);

        TemplateColorStyle ArchiveSpotColor(long SpotColorId);
    }
}
