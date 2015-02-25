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
        List<FieldVariable> GetVariablesData(bool isRealestateproduct, long storeId);
        Stream GetTemplateVariables(long templateId);
    }
}
