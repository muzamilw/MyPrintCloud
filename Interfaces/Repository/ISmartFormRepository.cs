﻿using MPC.Common;
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
        List<VariableList> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId);
        List<TemplateVariablesObj> GetTemplateVariables(long templateId);
        bool SaveTemplateVariables(List<TemplateVariablesObj> obj);
        List<SmartFormUserList> GetUsersList(long contactId);
    }
}
