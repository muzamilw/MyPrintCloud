using MPC.Models.Common;
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
        List<TemplateFontResponseModel> GetFontList(long productId, long customerId, long OrganisationID, long territoryId);

        void DeleteTemplateFonts(long Companyid, long OrganisationID);

        List<TemplateFont> GetFontList();

        List<TemplateFont> GetFontListForTemplate(long templateId);
        void InsertFontFile(long customerId, long organisationId, string FontName, string fontDisplayName);
    }
}
