using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    public interface ITemplateFontService
    {
        TemplateFont GetTemplateFontById(long fontId);
        IEnumerable<TemplateFont> GetTemplateFontsByStore(long companyId);
        IEnumerable<TemplateFont> GetTemplateFontsByTerritory(long territoryId);
        TemplateFont SaveTemplateFont(TemplateFont templateFont);
        bool DeleteTemplateFont(long fontId);
    }
}
