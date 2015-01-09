using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplateService
    {
        Template GetTemplate(int productID);

        Template GetTemplateInDesigner(int productID);

        List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID);
        string GetTemplateNameByTemplateID(int tempID);

        

        int CloneTemplateByTemplateID(int TempID);
    }
}
