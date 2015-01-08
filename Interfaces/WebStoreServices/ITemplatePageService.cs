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
    }
}
