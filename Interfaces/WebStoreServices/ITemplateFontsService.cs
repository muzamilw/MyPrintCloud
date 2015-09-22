using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplateFontsService
    {
        List<TemplateFont> GetFontList(long productId, long customerId, long OrganisationID);

        void DeleteTemplateFonts(long Companyid, long OrganisationID);

        List<TemplateFont> GetFontList();

        List<TemplateFont> GetFontListForTemplate(long templateId);
        void InsertFontFile(long customerId, long organisationId, string FontName, string fontDisplayName);
    }
}
