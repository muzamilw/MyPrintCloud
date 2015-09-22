using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ITemplateFontsRepository : IBaseRepository<TemplateFont, int>
    {
        List<TemplateFont> GetFontList(long productId, long customerId);
        void DeleteTemplateFonts(long Companyid);
        List<TemplateFont> GetFontList();
        void InsertFontFile(TemplateFont objFont);

        List<TemplateFont> getTemplateFontsByCompanyID(long CustomerID);

        List<TemplateFont> getTemplateFonts();
        List<TemplateFont> GetFontListForTemplate(long templateId);
    }
}
