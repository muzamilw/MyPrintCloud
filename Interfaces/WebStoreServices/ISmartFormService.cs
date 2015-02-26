using MPC.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ISmartFormService
    {
        List<VariableList> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId);
        List<TemplateVariablesObj> GetTemplateVariables(long templateId);
        bool SaveTemplateVariables(List<TemplateVariablesObj> obj);
    }
}
