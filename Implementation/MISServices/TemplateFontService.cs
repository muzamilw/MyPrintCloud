using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class TemplateFontService : ITemplateFontService
    {
        private readonly ITemplateFontsRepository _templateFontsRepository;

        public TemplateFontService(ITemplateFontsRepository templateFontsRepository)
        {
            if(templateFontsRepository == null)
                throw new ArgumentNullException("templateFontsRepository");
            this._templateFontsRepository = templateFontsRepository;
        }
        public TemplateFont GetTemplateFontById(long fontId)
        {
            return _templateFontsRepository.GetTemplateFontById(fontId);
        }

        public IEnumerable<TemplateFont> GetTemplateFontsByStore(long companyId)
        {
            return _templateFontsRepository.getTemplateFontsByCompanyID(companyId).Where(c => c.TerritoryId == null).ToList();
        }

        public IEnumerable<TemplateFont> GetTemplateFontsByTerritory(long territoryId)
        {
            return _templateFontsRepository.GetTemplateFontsByTerritory(territoryId);

        }

        public TemplateFont SaveTemplateFont(TemplateFont templateFont)
        {
            try
            {
                TemplateFont targetFont = GetTemplateFontById(templateFont.ProductFontId) ?? CreateNewFont();
                UpdateFont(targetFont, templateFont);
                _templateFontsRepository.SaveChanges();

                return targetFont;
            }
            catch (Exception exp)
            {
                throw new MPCException("Failed to save template font. Error: " + exp.Message, _templateFontsRepository.OrganisationId);
            }
        }

        private TemplateFont CreateNewFont()
        {
            TemplateFont newFont = _templateFontsRepository.Create();
            _templateFontsRepository.Add(newFont);
            return newFont;
        }
        private static void UpdateFont(TemplateFont target, TemplateFont source)
        {
            target.FontName = source.FontName;
            //target.FontFile = source.FontFile;
            target.IsEnable = source.IsEnable;
        }
    }
}
