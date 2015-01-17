using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplatePageService
    {
        List<TemplatePage> GetTemplatePages(long productId);
        List<TemplatePage> GetTemplatePagesSP(long productId);

        bool CreateBlankBackgroundPDFs(long TemplateID, double height, double width, int Orientation, long OrganisationID);
        bool CreateBlankBackgroundPDFsByPages(long TemplateID, double height, double width, int Orientation, List<TemplatePage> PagesList, long OrganisationID);
        string CreatePageBlankBackgroundPDFs(long TemplateID, TemplatePage oPage, double height, double width, long OrganisationID);

        bool DeleteBlankBackgroundPDFsByPages(long TemplateID, List<TemplatePage> PagesList, long OrganisationID);
    }
}
