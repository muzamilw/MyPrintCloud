using MPC.Models.DomainModels;
using System.Collections.Generic;
using System.IO;

namespace MPC.Interfaces.Repository
{

    /// <summary>
    /// Smart Form Repository Interface
    /// </summary>
    public interface ISmartFormRepository : IBaseRepository<SmartForm, long>
    {
        List<FieldVariable> GetVariablesData(bool isRealestateproduct, long storeId);
        Stream GetTemplateVariables(long templateId);
    }
}
